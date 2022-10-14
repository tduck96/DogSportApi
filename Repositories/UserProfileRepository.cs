using System;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RealPetApi.Dtos;
using RealPetApi.Models;

namespace RealPetApi.Repositories
{
    public class UserProfileRepository : IUserProfileRespository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public UserProfileRepository(DataContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
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

        public async Task<List<UserListDto>> GetUsers()
        {
           var users = await _context.UserProfiles
                .Include(c => c.Location)
                .Include(c => c.Photos)
                .ToListAsync();


            List<UserListDto> Dtos = new List<UserListDto>();

            foreach(UserProfile user in users)
            {
               
                
                var dto = new UserListDto
                {
                    Id = user.Id,
                    Bio = user.Bio,
                    Name = user.Name,
                    Location = user.Location.Name,
                 
                };

                Dtos.Add(dto);
            }
            return Dtos;
           


        }

        public async Task<bool> UpdateUserInfo(UserProfile user)
        {
            _context.UserProfiles.Update(user);
            var updated = await _context.SaveChangesAsync();
            return updated > 0;
        }
    }
}

