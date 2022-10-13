using System;
using Microsoft.EntityFrameworkCore;
using RealPetApi.Dtos;
using RealPetApi.Models;

namespace RealPetApi.Repositories
{
    public class UserProfileRepository : IUserProfileRespository
    {
        private readonly DataContext _context;

        public UserProfileRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateUser(UserProfile user)
        {
            await _context.UserProfiles.AddAsync(user);
            var created = await _context.SaveChangesAsync();
            return created > 0;
        }

        public Task<List<Dog>> GetDogsByUser(int userId)
        {
            throw new NotImplementedException();
        }

        public Task<List<PhotoDto>> GetPhotosByUser(int userId)
        {
            throw new NotImplementedException();
        }

        public async Task<UserProfile> GetUser(int userId)
        {
            return await _context.UserProfiles.Where(c => c.Id == userId).FirstOrDefaultAsync();
        }

        public Task<Handler> GetUserHandler(int userId)
        {
            throw new NotImplementedException();
        }

        public Task<List<UserProfile>> GetUsers()
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateUserInfo(UserProfile user)
        {
            _context.UserProfiles.Update(user);
            var updated = await _context.SaveChangesAsync();
            return updated > 0;
        }
    }
}

