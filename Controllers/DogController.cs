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

        public DogController(IDogRepository dogRepository,
            IMapper mapper)
        {
           _dogRepository = dogRepository;
            _mapper = mapper;
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

        [HttpGet("{dogName}")]
        [ProducesResponseType(200, Type = typeof(Dog))]
        [ProducesResponseType(400)]

        public IActionResult GetDogByName(string dogName)
        {
            if (!_dogRepository.DogExistsByName(dogName))
                return NotFound();

            var dog = _mapper.Map<DogDto>(_dogRepository.GetDogByName(dogName));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(dog);
        }

    }
}

