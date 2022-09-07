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
        private readonly ILocationRepository _locationRepository;

        public ClubController(IClubRepository clubRepository,
            IMapper mapper,
            ILocationRepository locationRepository)
        {
            _clubRepository = clubRepository;
            _mapper = mapper;
            _locationRepository = locationRepository;
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

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]

        public IActionResult CreateClub([FromQuery] int locationId, [FromBody] ClubDto clubCreate)
        {
            if (clubCreate == null)
                return BadRequest(ModelState);

            var club = _clubRepository.GetClubs()
                 .Where(b => b.Name.Trim().ToUpper() == clubCreate.Name.TrimEnd().ToUpper())
                 .FirstOrDefault();

            if (club != null)
            {
                ModelState.AddModelError("", "Breed already exits.");
                return StatusCode(422, ModelState);
            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var clubMap = _mapper.Map<Club>(clubCreate);


            clubMap.Location = _locationRepository.GetLocation(locationId);

            if (!_clubRepository.CreateClub(clubMap))
            {
                ModelState.AddModelError("", "Something went wrong with save");
                return StatusCode(500, ModelState);
            }
            return Ok("Sucessfully added new club to records");
        }

        [HttpPut("{clubId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateClub(int clubId, [FromBody] ClubDto updatedClub)
        {
            if (updatedClub == null)
                return BadRequest(ModelState);

            if (clubId != updatedClub.Id)
                return BadRequest(ModelState);

            if (!_clubRepository.ClubExists(clubId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var clubMap = _mapper.Map<Club>(updatedClub);

            if (!_clubRepository.UpdateClub(clubMap))
            {
                ModelState.AddModelError("", "Something went wrong updating record");
                return StatusCode(500, ModelState);

            }
            return NoContent();
        }

        [HttpDelete("{clubId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]

        public IActionResult DeleteClub(int clubId)
        {
            if (!_clubRepository.ClubExists(clubId))
            {
                return NotFound();
            }

            var clubToDelete = _clubRepository.GetClub(clubId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_clubRepository.DeleteClub(clubToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting category");

            }
            return NoContent();
        }

    }
}


