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

            var mappedDogs = _mapper.Map<List<DogDtoForUserProfile>>(Dogs);
            var mappedLocation = _mapper.Map<LocationDto>(Location);



            var toReturn = new HandlerProfileDto
            {
                Id = Handlers.Id,
                Name = Handlers.Name,
                Username = Handlers.Username,
                Dogs = mappedDogs,
                Location = mappedLocation
            };

            return toReturn;

        }


        public async Task<bool> UpdateProfile(int handlerId)
        {
            throw new NotImplementedException();
        }
    }
}

