using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;
using RealPetApi.Dtos;
using RealPetApi.Models;

namespace RealPetApi.Services
{
    public class AuthService : IAuthService
    {
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthService(DataContext context,
            IConfiguration configuration,
            IHttpContextAccessor httpContextAccessor
           )
        {
            _context = context;
            _configuration = configuration;
           _httpContextAccessor = httpContextAccessor;
        }



        public async Task<Handler> RegisterUser(AuthHandlerDto request)
        {
            CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

            var handler = new Handler
            {
                Username = request.Username,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                LocationId = 1
            };


            _context.Handlers.Add(handler);
            await _context.SaveChangesAsync();


            return handler;
        }

        public async Task<AuthResponseDto> LoginUser(AuthHandlerDto request)
        {
        
                var user = await _context.Handlers.FirstOrDefaultAsync(u => u.Username == request.Username);

                if (user == null)
                {
                    return new AuthResponseDto { Message = "User not found" };

                }

                if (!VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
                {
                    return new AuthResponseDto { Message = "Wrong password" };
                }

                string token = CreateToken(user);
                var refreshToken = CreateRefreshToken();
                await SetRefreshToken(refreshToken, user);
           

                return new AuthResponseDto
                {
                    Success = true,
                    Token = token,
                    RefreshToken = refreshToken.Token,
                    TokenExpires = refreshToken.Expires
                };

            }

        public async Task<AuthResponseDto> RefreshToken()
        {
            var refreshToken = _httpContextAccessor?.HttpContext?.Request.Cookies["refreshToken"];

            var handler = await _context.Handlers.FirstOrDefaultAsync(u => u.RefreshToken == refreshToken);

            if (handler == null)
            {
                return new AuthResponseDto { Message = "Invalid Refresh Token" };
            }
            else if (handler.TokenExpires < DateTime.Now)
            {
                return new AuthResponseDto { Message = "Token Expired" };
            }

            string token = CreateToken(handler);
            var newRefreshToken = CreateRefreshToken();
            await SetRefreshToken(newRefreshToken, handler);

            return new AuthResponseDto
            {
                Success = true,
                Token = token,
                RefreshToken = newRefreshToken.Token,
                TokenExpires = newRefreshToken.Expires

            };
        }

        public async Task<AuthResponseDto> LogoutUser()
        {
            var refreshToken = _httpContextAccessor?.HttpContext?.Request.Cookies["refreshToken"];

            var handler = await _context.Handlers.FirstOrDefaultAsync(u => u.RefreshToken == refreshToken);

            if (handler == null)
            {
                return new AuthResponseDto { Message = "Invalid Refresh Token" };
            }

            await LogoutUser(handler);

            return new AuthResponseDto
            {
                Success = true,
            };


        }

       




        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using var hmac = new HMACSHA512(passwordSalt);
            var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            return computedHash.SequenceEqual(passwordHash);
        }

        private string CreateToken(Handler handler)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, handler.Id.ToString()),
                new Claim(ClaimTypes.Name, handler.Username),
                new Claim(ClaimTypes.Role, handler.Role)
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        private RefreshTokenDto CreateRefreshToken()
        {
            var refreshToken = new RefreshTokenDto
            {
                Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                Expires = DateTime.Now.AddDays(7),
                Created = DateTime.Now
            };

            return refreshToken;
        }

        private async Task SetRefreshToken(RefreshTokenDto refreshToken, Handler handler)
        {
            var cookieOptions = new CookieOptions
            {
                Path = "/",
                IsEssential = true,
                HttpOnly = true,
                Expires = refreshToken.Expires,
                Secure = false

            };

            _httpContextAccessor?.HttpContext?.Response
                .Cookies.Append("refreshToken", refreshToken.Token, cookieOptions);

            handler.RefreshToken = refreshToken.Token;
            handler.TokenCreated = refreshToken.Created;
            handler.TokenExpires = refreshToken.Expires;

            await _context.SaveChangesAsync();

        }

        private async Task LogoutUser(Handler handler)
        {
            var cookieOptions = new CookieOptions
            {
                Path = "/",
                IsEssential = true,
                HttpOnly = true,
                Secure = false
            };

            _httpContextAccessor?.HttpContext?.Response
                .Cookies.Delete("refreshToken");

            handler.RefreshToken = string.Empty;
            

            await _context.SaveChangesAsync();
        }

    }
}


