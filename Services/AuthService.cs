using System;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
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



        public async Task<AuthResponseDto> RegisterUser(AuthHandlerDto request)
        {
            var regex = IsValidEmail(request.Email);

            if (regex == false) return new AuthResponseDto { Message = "Please enter a valid email" };

            if (request.Password.Length < 7) return new AuthResponseDto { Message = "Password must contain at least 7 characters." };

            var user = await _context.Handlers.FirstOrDefaultAsync(h => h.Email == request.Email);

             if (user != null)
            {
                return new AuthResponseDto {
                    Success = false,
                    Message = "Account already exists with that username"

                };
            }

            else
            {

                CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);
                {
                    var handler = new Handler
                    {
                        Email = request.Email,
                        PasswordHash = passwordHash,
                        PasswordSalt = passwordSalt,
                    };


                    _context.Handlers.Add(handler);


                    await _context.SaveChangesAsync();



                    return new AuthResponseDto
                    {
                        Success = true,
                        Message = "Welcome!",
                        Id = handler.Id
                       
                    };
                }
            };

        }

        public async Task<AuthResponseDto> LoginUser(AuthHandlerDto request)
        {
            var user = await _context.Handlers.Where(c => c.Email == request.Email).FirstOrDefaultAsync();

            if (user == null) return new AuthResponseDto { Message = "No account found with that username" };


            if (!VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
            {
                return new AuthResponseDto { Message = "Invalid password" };
            }

            var profile = await _context.UserProfiles.Where(c => c.HandlerId == user.Id).FirstOrDefaultAsync();

            string token = CreateToken(user);
           
            var refreshToken = CreateRefreshToken();

            await SetRefreshToken(refreshToken, user);





            return new AuthResponseDto
            {
                Success = true,
                Token = token,
                RefreshToken = refreshToken.Token,
                TokenExpires = refreshToken.Expires,
                Id = user.Id,
                UserId = profile.Id
            };

        }

        public async Task<AuthResponseDto> RefreshToken()
        {
            var refreshToken = _httpContextAccessor?.HttpContext?.Request.Cookies["refreshToken"];

            var handler = await _context.Handlers.FirstOrDefaultAsync(u => u.RefreshToken == refreshToken);
            var profile = await _context.UserProfiles.Where(c => c.HandlerId == handler.Id).FirstOrDefaultAsync();

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
                TokenExpires = newRefreshToken.Expires,
                Id = handler.Id,
                UserId = profile.Id



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
                new Claim(ClaimTypes.Email, handler.Email),
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
                Secure = true,
                SameSite = SameSiteMode.None


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
                Secure = true,
                SameSite = SameSiteMode.None
            };

            _httpContextAccessor?.HttpContext?.Response
                .Cookies.Delete("refreshToken");

            handler.RefreshToken = string.Empty;


            await _context.SaveChangesAsync();
        }

        public static bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                // Normalize the domain
                email = Regex.Replace(email, @"(@)(.+)$", DomainMapper,
                                      RegexOptions.None, TimeSpan.FromMilliseconds(200));

                // Examines the domain part of the email and normalizes it.
                string DomainMapper(Match match)
                {
                    // Use IdnMapping class to convert Unicode domain names.
                    var idn = new IdnMapping();

                    // Pull out and process domain name (throws ArgumentException on invalid)
                    string domainName = idn.GetAscii(match.Groups[2].Value);

                    return match.Groups[1].Value + domainName;
                }
            }
            catch (RegexMatchTimeoutException e)
            {
                return false;
            }
            catch (ArgumentException e)
            {
                return false;
            }

            try
            {
                return Regex.IsMatch(email,
                    @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                    RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }

    }
}


