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

        public async Task<List<DogDtoForUserProfile>> GetDogsByUser(int userId)
        {
            var dogs = await _context.Dogs.Where(c => c.UserProfileId == userId).ToListAsync();

            var dogMap = _mapper.Map<List<DogDtoForUserProfile>>(dogs);

            return dogMap;
        }

        public async Task<List<PhotoDto>> GetPhotosByUser(int userId)
        {
            var photos = await _context.Photos.Where(c => c.UserProfileId == userId).ToListAsync();

            var photoMap = _mapper.Map<List<PhotoDto>>(photos);

            return photoMap;
        }

        public async Task<UserProfile> GetUser(int userId)
        {
           return await _context.UserProfiles.Where(c => c.Id == userId).Include(c => c.Location).FirstOrDefaultAsync();

        }

        public async Task<Handler> GetUserHandler(int userId)
        {
            var user = await GetUser(userId);
            var handlerId = user.HandlerId;

            return await _context.Handlers.Where(c => c.Id == handlerId).FirstOrDefaultAsync();
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
                var photo = await _context.Photos.Where(c => c.UserProfileId == user.Id)
                    .Select(c => c.Url).ToListAsync();

                
                var dto = new UserListDto
                {
                    Id = user.Id,
                    Bio = user.Bio,
                    Name = user.Name,
                    Location = user.Location.Name,
                    Urls = photo
                 
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

        public async Task<List<WallPostProfileDto>> GetWallPostsByUser(int userId)
        {
            var wallposts = await _context.Wallposts.Where(c => c.UserProfileId == userId).ToListAsync();
            var wallPostMap = _mapper.Map<List<WallPostProfileDto>>(wallposts);
          
            return wallPostMap;
        }

    }
}

