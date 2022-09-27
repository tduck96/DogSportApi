using System;
using System.Security.Cryptography;
using RealPetApi.Dtos;
using RealPetApi.Models;

namespace RealPetApi.Services
{
    public class AuthService : IAuthService
    {
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;
       

        public AuthService(DataContext context,
            IConfiguration configuration
           )
        {
            _context = context;
            _configuration = configuration;
            
        }



        public async Task<Handler> RegisterUser(AuthHandlerDto request)
        {
            CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

            var handler = new Handler
            {
                Username = request.Username,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                LocationId = request.LocationId
            };


            _context.Handlers.Add(handler);
            await _context.SaveChangesAsync();


            return handler;
        }

        public Task<AuthResponseDto> LoginUser(AuthHandlerDto request)
        {
            throw new NotImplementedException();
        }

        public Task<AuthResponseDto> LogoutUser()
        {
            throw new NotImplementedException();
        }

        public Task<AuthResponseDto> RefreshToken()
        {
            throw new NotImplementedException();
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
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }

    }
}


