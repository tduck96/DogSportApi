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

        public async Task<ActionResult<ClubDto>> GetClubs()
        {
            var clubs = await _clubRepository.GetClubs();
            var clubsToReturn = _mapper.Map<List<ClubDto>>(clubs);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(clubsToReturn);


        }

        [HttpGet("{clubId}")]
        [ProducesResponseType(200, Type = typeof(Club))]
        [ProducesResponseType(400)]

        public async Task<ActionResult<ClubDto>> GetClub(int clubId)
        {
            var club = await _clubRepository.GetClub(clubId);
            var clubToReturn = _mapper.Map<ClubDto>(club);
  

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(club);
        }

        [HttpGet("sports/{clubId}")]
        [ProducesResponseType(200, Type = typeof(Sport))]
        [ProducesResponseType(400)]

        public async Task<ActionResult<SportDto>> GetSportsByClub(int clubId)
        {
            var club = await _clubRepository.GetClub(clubId);
            if (club == null)
                return NotFound();

            var sports = await _clubRepository.GetSportsByClub(clubId);

            var sportsToReturn = _mapper.Map<List<SportDto>>(sports);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(sportsToReturn);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]

        public async Task<ActionResult<Club>> CreateClub([FromQuery] int locationId , [FromBody] ClubDto clubCreate)
        {
            if (clubCreate == null)
                return BadRequest(ModelState);

            var club = await _clubRepository.GetClub(clubCreate.Id);

            if (club != null)
            {
                ModelState.AddModelError("", "Club Already Exists");
                    return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var clubMap = _mapper.Map<Club>(clubCreate);

            clubMap.Location = await _locationRepository.GetLocation(locationId);

            await _clubRepository.CreateClub(clubMap);

            return Ok("Sucessfully added Club to Records");

        }


        [HttpPut("{clubId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> UpdateClub([FromBody] ClubDto request)
        {


            var club = new Club
            {
                Id = request.Id,
                Name = request.Name,
                LocationId = request.LocationId
            };

            var updatedClub = await _clubRepository.UpdateClub(club);

            if (updatedClub)
                return Ok("Record Updated");

            return NotFound();
        }


           [HttpDelete("{clubId}")]
           [ProducesResponseType(400)]
           [ProducesResponseType(204)]
           [ProducesResponseType(404)]

           public async Task<ActionResult> DeleteClub(int clubId)
        {
            var clubToDelete = await _clubRepository.GetClub(clubId);

            if (clubToDelete != null)
            {
                await _clubRepository.DeleteClub(clubId);
                return Ok("Club Removed");
            }
            return NotFound();
           
        }

    }

}
