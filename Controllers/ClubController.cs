using System;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        [ProducesResponseType(200, Type = typeof(IEnumerable<ClubDto>))]

        public async Task<ActionResult<ClubDto>> GetClubs()
        {
            var clubs = await _clubRepository.GetClubDtos();
           
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(clubs);


        }

        [HttpGet("{clubId}")]
        [ProducesResponseType(200, Type = typeof(ClubDto))]
        [ProducesResponseType(400)]

        public async Task<ActionResult<ClubDto>> GetClub(int clubId)
        {
            var club = await _clubRepository.GetClub(clubId);

            if (club == null)
                return NotFound();


            var clubDto = new ClubDto
            {
                Id = club.Id,
                Name = club.Name,
                About = "We are a dog training facility!",
                Location = club.Location.Name,
                PhotoUrl = "https://res.cloudinary.com/dx58mbwcg/image/upload/v1668193137/Screen_Shot_2022-11-11_at_12.58.24_PM_nsq3za.png",
                Founded = 2015,
 
            };
      
            return Ok(clubDto);
        }

        [HttpGet("sports/{clubId}")]
        [ProducesResponseType(200, Type = typeof(SportDto))]
        [ProducesResponseType(400)]

        public async Task<ActionResult<SportDto>> GetSportsByClub(int clubId)
        {
            var club = await _clubRepository.GetSportsByClub(clubId);
            if (club == null)
                return NotFound();

            var sports = await _clubRepository.GetSportsByClub(clubId);

            var sportsToReturn = _mapper.Map<List<SportDto>>(sports);


            return Ok(sportsToReturn);
        }

       

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]

        public async Task<ActionResult<ClubCreateDto>> CreateClub([FromBody] ClubCreateDto clubCreate)
        {
            if (clubCreate == null)
                return BadRequest(ModelState);

            var clubMap = _mapper.Map<Club>(clubCreate);

            clubMap.Location = await _locationRepository.GetLocation(clubCreate.LocationId);

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
                Name = request.Name
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
