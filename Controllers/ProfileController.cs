using System;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RealPetApi.Dtos;
using RealPetApi.Models;
using RealPetApi.Services;

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
        private readonly IUploadService _uploadService;

        public ProfileController(IHandlerRepository handlerRepository,
         IMapper mapper,
         ILocationRepository locationRepository,
         IBreedRepository breedRepository,
         IProfileRepository profileRepository,
         IUploadService uploadService
          
         )
        {
            _handlerRepository = handlerRepository;
            _mapper = mapper;
            _locationRepository = locationRepository;
            _breedRepository = breedRepository;
            _profileRepository = profileRepository;
            _uploadService = uploadService;
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

        [HttpPost("{handlerId}")]

        public async Task<ActionResult<bool>> UploadPhoto(IFormFile file)
        {
            var result = await _uploadService.AddPhotoAsync(file);

            if (result.Error != null) return BadRequest(result.Error.Message);

            var photo = new Photo
            {
                Url = result.SecureUrl.AbsoluteUri,
                PublicId = result.PublicId

            };


            return Ok();
        }

    }
}

