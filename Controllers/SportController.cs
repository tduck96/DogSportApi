using System;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RealPetApi.Dtos;
using RealPetApi.Models;

namespace RealPetApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class SportController : Controller
    {
        private readonly ISportRepository _sportRepository;
        private readonly IMapper _mapper;

        public SportController(ISportRepository sportRepository,
            IMapper mapper)
        {
            _sportRepository = sportRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Sport>))]

        public IActionResult GetSports()
        {
            var sports = _mapper.Map<List<SportDto>>(_sportRepository.GetSports());


            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(sports);
        }

        [HttpGet("{sportId}")]
        [ProducesResponseType(200, Type = typeof(Sport))]
        [ProducesResponseType(400)]

        public IActionResult GetSport(int sportId)
        {
            if (!_sportRepository.SportExists(sportId))
                return NotFound();

            var sport = _mapper.Map<SportDto>(_sportRepository.GetSport(sportId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(sport);
        }

        [HttpGet("clubs/{sportId}")]
        [ProducesResponseType(200, Type = typeof(Club))]
        [ProducesResponseType(400)]

        public IActionResult GetClubsBySport(int sportId)
        {
            if (!_sportRepository.SportExists(sportId))
                return NotFound();

            var clubs = _mapper.Map<List<ClubDto>>(
                _sportRepository.GetClubsBySport(sportId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(clubs);
        }

        [HttpGet("handlers/{sportId}")]
        [ProducesResponseType(200, Type = typeof(Handler))]
        [ProducesResponseType(400)]

        public IActionResult GetHandlersBySport(int sportId)
        {
            if (!_sportRepository.SportExists(sportId))
                return NotFound();

            var handlers = _mapper.Map<List<HandlerDto>>(
                _sportRepository.GetHandlersBySport(sportId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(handlers);
        }

        [HttpGet("dogs/{sportId}")]
        [ProducesResponseType(200, Type = typeof(Dog))]
        [ProducesResponseType(400)]

        public IActionResult GetDogsBySport(int sportId)
        {
            if (!_sportRepository.SportExists(sportId))
                return NotFound();

            var dogs = _mapper.Map<List<DogDto>>(
                _sportRepository.GetDogsBySport(sportId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(dogs);
        }
    }
}

