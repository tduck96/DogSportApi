using System;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RealPetApi.Dtos;
using RealPetApi.Models;

namespace RealPetApi.Data
{
    [Route("api/[controller]")]
    [ApiController]

    public class ClubController : Controller
    {
        private readonly IClubRepository _clubRepository;
        private readonly IMapper _mapper;

        public ClubController(IClubRepository clubRepository,
            IMapper mapper)
        {
            _clubRepository = clubRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Club>))]

        public IActionResult GetClubs()
        {
            var clubs = _mapper.Map<List<ClubDto>>(_clubRepository.GetClubs());


            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(clubs);
        }

        [HttpGet("{clubId}")]
        [ProducesResponseType(200, Type = typeof(Club))]
        [ProducesResponseType(400)]

        public IActionResult GetBreed(int clubId)
        {
            if (!_clubRepository.ClubExists(clubId))
                return NotFound();

            var club = _mapper.Map<ClubDto>(_clubRepository.GetClub(clubId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(club);
        }

        [HttpGet("sports/{clubId}")]
        [ProducesResponseType(200, Type = typeof(Sport))]
        [ProducesResponseType(400)]

        public IActionResult GetSportsByClub(int clubId)
        {
            if (!_clubRepository.ClubExists(clubId))
                return NotFound();

            var sports = _mapper.Map<List<SportDto>>(
                _clubRepository.GetSportsByClub(clubId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(sports);
        }


    }
}


