using System;
using AutoMapper;
using RealPetApi.Dtos;
using RealPetApi.Models;

namespace RealPetApi.Repositories
{
    public class ProfileRepository : IProfileRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public ProfileRepository(DataContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        public async Task<HandlerProfileDto> GetProfile(int handlerId)
        {
            var Handlers = await _context.Handlers.FirstOrDefaultAsync(c => c.Id == handlerId);
            var Dogs = await _context.Dogs.Where(c => c.HandlerId == handlerId).ToListAsync();  
            var Location = await _context.Locations.Where(c => c.Id == Handlers.LocationId).FirstOrDefaultAsync();
            var Wallposts = await GetWallPostsByProfile(handlerId);
            var Photos = await _context.Photos.Where(c => c.HandlerId == handlerId).ToListAsync();

            var mappedDogs = _mapper.Map<List<DogDtoForUserProfile>>(Dogs);
            var mappedLocation = _mapper.Map<LocationDto>(Location);
            var mappedWallposts = _mapper.Map<List<WallPostDto>>(Wallposts);
            var mappedPhoto = _mapper.Map<List<PhotoDto>>(Photos);


            var toReturn = new HandlerProfileDto
            {
                Id = Handlers.Id,
                Name = Handlers.Name,
                Username = Handlers.Username,
                Dogs = mappedDogs,
                Location = mappedLocation,
                WallPosts = mappedWallposts,
                Photos = mappedPhoto
            };

            return toReturn;

        }

        public async Task<List<WallPost>> GetWallPostsByProfile(int handlerId)
        {
            return await _context.Wallposts.Where(c => c.HandlerId == handlerId).ToListAsync();
        }

        public async Task<bool> UpdateProfile(int handlerId)
        {
            throw new NotImplementedException();
        }
    }
}

