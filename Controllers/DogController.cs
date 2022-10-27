using System;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RealPetApi.Dtos;
using RealPetApi.Interfaces;
using RealPetApi.Models;
using RealPetApi.Services;

namespace RealPetApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class DogController : Controller
    {
        private readonly IDogRepository _dogRepository;
        private readonly IMapper _mapper;
        private readonly ILocationRepository _locationRepository;
        private readonly IBreedRepository _breedRepository;
        private readonly IHandlerRepository _handlerRepository;
        private readonly IUploadService _uploadService;
        private readonly IDogPhotoRepository _dogPhotoRepository;
        private readonly IUserProfileRespository _userProfileRespository;

        public DogController(IDogRepository dogRepository,
            IMapper mapper,
            ILocationRepository locationRepository,
            IBreedRepository breedRepository,
            IHandlerRepository handlerRepository,
            IUploadService uploadService,
            IDogPhotoRepository dogPhotoRepository,
            IUserProfileRespository userProfileRespository

            )
        {
           _dogRepository = dogRepository;
            _mapper = mapper;
           _locationRepository = locationRepository;
           _breedRepository = breedRepository;
            _handlerRepository = handlerRepository;
            _uploadService = uploadService;
            _dogPhotoRepository = dogPhotoRepository;
            _userProfileRespository = userProfileRespository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Dog>))]

        public async Task<ActionResult<DogDto>> GetDogs()
        {
            var dogs = await _dogRepository.GetDogs();
            var dogsToReturn = _mapper.Map<List<DogDto>>(dogs);


            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(dogsToReturn);
        }


        [HttpGet("{dogId}")]
        [ProducesResponseType(200, Type = typeof(Dog))]
        [ProducesResponseType(400)]

        public async Task<ActionResult<DogProfileDto>> GetDog(int dogId)
        {
            var dog = await _dogRepository.GetDog(dogId);
            var breed = await _breedRepository.GetBreed(dog.BreedId);
            var sports = await _dogRepository.GetSportsByDog(dogId);
            var photos = await _dogRepository.GetPhotosByDog(dogId);
           

            var dogProfileDto = new DogProfileDto
            {
                Id = dog.Id,
                Name = dog.Name,
                PhotoUrl = dog.PhotoUrl,
                Weight = dog.Weight,
                Sports = sports,
                DogPhotos = photos
            };
           

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(dogProfileDto);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]

        public async Task <ActionResult<bool>> CreateDog([FromQuery] int locationId,
            [FromQuery] int userId,
            [FromQuery] int breedId,
            [FromBody] DogDto dogCreate)


        {
            if (dogCreate == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var dogMap = _mapper.Map<Dog>(dogCreate);


            dogMap.Location = await _locationRepository.GetLocation(locationId);
            dogMap.Breed = await _breedRepository.GetBreed(breedId);
            dogMap.UserProfile = await _userProfileRespository.GetUser(userId);

            await _dogRepository.CreateDog(dogMap);

            return Ok("Sucessfully added new dog to records");
        }


        [HttpPut("{dogId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
            public async Task<ActionResult> UpdateDog(int dogId, [FromBody] DogDto updatedDog)
        {
            var dog = new Dog
            {
                Id = updatedDog.Id,
                Name = updatedDog.Name,
                About = updatedDog.About,
                PhotoUrl = updatedDog.PhotoUrl,
                BreedId = 1,
                LocationId = 1,
                UserProfileId = updatedDog.UserProfileId
               
            };

            var updated= await _dogRepository.UpdateDog(dog);

            if (updated)
                return Ok("Record Updated");
            return NotFound();
           

        }

        [HttpDelete("{dogId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]

        public async Task<ActionResult> DeleteDog(int dogId)
        {
            var deleted = await _dogRepository.DeleteDog(dogId);
            if (deleted)
                return Ok("Dog is deleted");

            return NotFound();
            
        }

        [HttpPost("addphoto/")]

        public async Task<ActionResult<bool>> UploadPhoto(IFormFile file)
        {
            var result = await _uploadService.AddPhotoAsync(file);

            //if (result.Error != null) return BadRequest(result.Error.Message);


            var photo = new DogPhoto
            {
                Url = result.SecureUrl.AbsoluteUri,
                PublicId = result.PublicId,

            };


            return Ok(photo.Url);
        }



    }
}

