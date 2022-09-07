using System;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RealPetApi.Dtos;
using RealPetApi.Interfaces;
using RealPetApi.Models;

namespace RealPetApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class BreedController : Controller
    {
        private readonly IBreedRepository _breedRepository;
        private readonly IMapper _mapper;

        public BreedController(IBreedRepository breedRepository,
            IMapper mapper)
        {
            _breedRepository = breedRepository;
            _mapper = mapper;
        }


        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Breed>))]

        public IActionResult GetBreeds()
        {
            var breeds = _mapper.Map<List<BreedDto>>(_breedRepository.GetBreeds());


            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(breeds);
        }


        [HttpGet("{breedId}")]
        [ProducesResponseType(200, Type = typeof(Breed))]
        [ProducesResponseType(400)]

        public IActionResult GetBreed(int breedId)
        {
            if (!_breedRepository.BreedExists(breedId))
                return NotFound();

            var breed = _mapper.Map<BreedDto>(_breedRepository.GetBreed(breedId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(breed);
        }

        [HttpGet("dogs/{breedId}")]
        [ProducesResponseType(200, Type = typeof(Dog))]
        [ProducesResponseType(400)]

        public IActionResult GetDogsByBreed(int breedId)
        {
            if (!_breedRepository.BreedExists(breedId))
                return NotFound();

            var dogs = _mapper.Map<List<DogDto>>(
                _breedRepository.GetDogsByBreed(breedId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(dogs);
        }
    }
}

