using System;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RealPetApi.Dtos;
using RealPetApi.Models;

namespace RealPetApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController: Controller
    {
        private readonly IHandlerRepository _handlerRepository;
        private readonly IMapper _mapper;
        private readonly ILocationRepository _locationRepository;
        private readonly IBreedRepository _breedRepository;
        private readonly IProfileRepository _profileRepository;

        public ProfileController(IHandlerRepository handlerRepository,
         IMapper mapper,
         ILocationRepository locationRepository,
         IBreedRepository breedRepository,
         IProfileRepository profileRepository
          
         )
        {
            _handlerRepository = handlerRepository;
            _mapper = mapper;
            _locationRepository = locationRepository;
            _breedRepository = breedRepository;
            _profileRepository = profileRepository;
        }

        [HttpGet("{handlerId}")]
        [ProducesResponseType(200, Type = typeof(Handler))]
        [ProducesResponseType(400)]

        public async Task<ActionResult<HandlerProfileDto>> GetProfile(int handlerId)
        {
            var handler = await _profileRepository.GetProfile(handlerId);
            if (handler != null)
            {

                return Ok(handler);
            }
            return NotFound();


        }

    }
}

