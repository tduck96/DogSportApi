using System;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RealPetApi.Dtos;
using RealPetApi.Interfaces;
using RealPetApi.Models;
using RealPetApi.Services;
using static System.Net.WebRequestMethods;

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
        private readonly ISportRepository _sportRepository;

        public DogController(IDogRepository dogRepository,
            IMapper mapper,
            ILocationRepository locationRepository,
            IBreedRepository breedRepository,
            IHandlerRepository handlerRepository,
            IUploadService uploadService,
            IDogPhotoRepository dogPhotoRepository,
            IUserProfileRespository userProfileRespository,
            ISportRepository sportRepository

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
            _sportRepository = sportRepository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Dog>))]

        public async Task<ActionResult<DogDto>> GetDogs()
        {
            var dogs = await _dogRepository.GetDogs();
            var dogsToReturn = _mapper.Map<List<DogDto>>(dogs);


            if (!ModelState.IsValid)
                return NotFound();


            return Ok(dogsToReturn);
        }

        [HttpGet("sports/{dogId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<SportDto>))]

        public async Task<ActionResult<SportDto>> GetSportsByDog(int dogId)
        {
            var sports = await _dogRepository.GetSportsByDog(dogId);

            return Ok(sports);
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
                About = dog.About,
                PhotoUrl = dog.PhotoUrl,
                Weight = dog.Weight,
                Titles = dog.Titles,
                Sports = sports,
                DogPhotos = photos,
                BreedName = breed.Name
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


            if (dogMap.PhotoUrl == "")
            {
                dogMap.PhotoUrl = "https://res.cloudinary.com/dx58mbwcg/image/upload/v1668044926/Dogsil_os0d2e.png";
            }


            dogMap.Breed = await _breedRepository.GetBreed(breedId);
            dogMap.UserProfile = await _userProfileRespository.GetUser(userId);

            await _dogRepository.CreateDog(dogMap);

            return Ok("Sucessfully added new dog to records");
        }


        [HttpPut("{dogId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
            public async Task<ActionResult<bool>> UpdateDog(int dogId, [FromBody] DogDto updatedDog)
        {
            var dog = new Dog
            {
                Id = updatedDog.Id,
                Name = updatedDog.Name,
                About = updatedDog.About,
                PhotoUrl = updatedDog.PhotoUrl,
                Titles = updatedDog.Titles,
                BreedId = updatedDog.BreedId,
                Weight = updatedDog.Weight,
                UserProfileId = updatedDog.UserProfileId
               
            };

            var updated= await _dogRepository.UpdateDog(dog);

            if (updated)
                return Ok("Record Updated");
            return NotFound();
           

        }

        [HttpPost("{dogId}/addsport")]
        public async Task<ActionResult<Sport>> AddSport(int dogId, [FromQuery] int sportsId)
        {
            var dog = await _dogRepository.GetDog(dogId);
            var sport = await _sportRepository.GetSport(sportsId);

            var newSport = new DogSport
            {
                DogId = dog.Id,
                SportId = sport.Id
            };

            await _dogRepository.AddSportsToDog(newSport);
          

            return Ok("New DogSport added successfully");

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

