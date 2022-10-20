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


        //public async Task<HandlerProfileDto> GetProfile(int userId)
        //{
        //    var Users = await _context.UserProfiles.FirstOrDefaultAsync(c => c.Id == userId);
        //    var Dogs = await _context.Dogs.Where(c => c.UserProfileId == userId).ToListAsync();  

        //    var Wallposts = await GetWallPostsByProfile(userId);
        //    var Photos = await _context.Photos.Where(c => c.UserProfileId == userId).ToListAsync();

        //    var mappedDogs = _mapper.Map<List<DogDtoForUserProfile>>(Dogs);

        //    var mappedWallposts = _mapper.Map<List<WallPostDto>>(Wallposts);
        //    var mappedPhoto = _mapper.Map<List<PhotoDto>>(Photos);


        //    var toReturn = new HandlerProfileDto
        //    {
        //        Id = Users.Id,
        //        Name = Users.Name,
        //        Dogs = mappedDogs,

        //        WallPosts = mappedWallposts,
        //        Photos = mappedPhoto
        //    };

        //    return HandlerProfileDto;

        //}

        //    public async Task<List<WallPost>> GetWallPostsByProfile(int userId)
        //    {
        //        return await _context.Wallposts.Where(c => c.UserProfileId == userId).ToListAsync();
        //    }

        //    public async Task<bool> UpdateProfile(int handlerId)
        //    {
        //        throw new NotImplementedException();
        //    }
        //}
    }
}
