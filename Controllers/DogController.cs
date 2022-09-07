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

        public IActionResult GetDogs()
        {
            var dogs = _mapper.Map<List<DogDto>>(_dogRepository.GetDogs());


            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(dogs);
        }


        [HttpGet("{dogId}")]
        [ProducesResponseType(200, Type = typeof(Dog))]
        [ProducesResponseType(400)]

        public IActionResult GetDog(int dogId)
        {
            if (!_dogRepository.DogExists(dogId))
                return NotFound();

            var dog = _mapper.Map<DogDto>(_dogRepository.GetDog(dogId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(dog);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]

        public IActionResult CreateDog([FromQuery] int locationId,
            [FromQuery] int handlerId,
            [FromQuery] int breedId,
            [FromBody] DogDto dogCreate)


        {
            if (dogCreate == null)
                return BadRequest(ModelState);

            var dog = _dogRepository.GetDogs()
                 .Where(b => b.Name.Trim().ToUpper() == dogCreate.Name.TrimEnd().ToUpper())
                 .FirstOrDefault();

            if (dog != null)
            {
                ModelState.AddModelError("", "Breed already exits.");
                return StatusCode(422, ModelState);
            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var dogMap = _mapper.Map<Dog>(dogCreate);


            dogMap.Location = _locationRepository.GetLocation(locationId);
            dogMap.Breed = _breedRepository.GetBreed(breedId);
            dogMap.Handler = _handlerRepository.GetHandler(handlerId);


            if (!_dogRepository.CreateDog(dogMap))
            {
                ModelState.AddModelError("", "Something went wrong with save");
                return StatusCode(500, ModelState);
            }
            return Ok("Sucessfully added new dog to records");
        }


        [HttpPut("{dogId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateDog(int dogId, [FromBody] DogDto updatedDog)
        {
            if (updatedDog == null)
                return BadRequest(ModelState);

            if (dogId != updatedDog.Id)
                return BadRequest(ModelState);

            if (!_dogRepository.DogExists(dogId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var dogMap = _mapper.Map<Dog>(updatedDog);

            if (!_dogRepository.UpdateDog(dogMap))
            {
                ModelState.AddModelError("", "Something went wrong updating record");
                return StatusCode(500, ModelState);

            }
            return NoContent();
        }

        [HttpDelete("{dogId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]

        public IActionResult DeleteDog(int dogId)
        {
            if (!_dogRepository.DogExists(dogId))
            {
                return NotFound();
            }

            var dogToDelete = _dogRepository.GetDog(dogId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_dogRepository.DeleteDog(dogToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting category");

            }
            return NoContent();
        }

    }
}

