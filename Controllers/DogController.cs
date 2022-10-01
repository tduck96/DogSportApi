using System;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RealPetApi.Dtos;
using RealPetApi.Models;

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

        public DogController(IDogRepository dogRepository,
            IMapper mapper,
            ILocationRepository locationRepository,
            IBreedRepository breedRepository,
            IHandlerRepository handlerRepository
            )
        {
           _dogRepository = dogRepository;
            _mapper = mapper;
           _locationRepository = locationRepository;
           _breedRepository = breedRepository;
            _handlerRepository = handlerRepository;
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

        public async Task<ActionResult<DogDto>> GetDog(int dogId)
        {
            var dog = await _dogRepository.GetDog(dogId);
            var dogToReturn = _mapper.Map<DogDto>(dog);
            
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(dog);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]

        public async Task <ActionResult<bool>> CreateDog([FromQuery] int locationId,
            [FromQuery] int handlerId,
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
            dogMap.Handler = await _handlerRepository.GetHandler(handlerId);

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
                BreedId = updatedDog.BreedId,
                HandlerId = updatedDog.Id
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

    }
}

