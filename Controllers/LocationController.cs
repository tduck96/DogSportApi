using System;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RealPetApi.Dtos;
using RealPetApi.Models;

namespace RealPetApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class LocationController : Controller
    {
        private readonly ILocationRepository _locationRepository;
        private readonly IMapper _mapper;

        public LocationController(ILocationRepository locationRepository,
            IMapper mapper)
        {
            _locationRepository = locationRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Location>))]

        public async Task<ActionResult<List<LocationDto>>> GetLocations()
        {
            var locations = await _locationRepository.GetLocations();
            var locationsToReturn = _mapper.Map<List<LocationDto>>(locations);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(locationsToReturn);
        }

        [HttpGet("{locationId}")]
        [ProducesResponseType(200, Type = typeof(Location))]
        [ProducesResponseType(400)]

        public async Task<ActionResult> GetLocation(int locationId)
        {
            var location = await _locationRepository.GetLocation(locationId);
            if (location == null)
                return NotFound();

            var locationToReturn = _mapper.Map<LocationDto>(location);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(locationToReturn);
        }

        [HttpGet("clubs/{locationId}")]
        [ProducesResponseType(200, Type = typeof(Club))]
        [ProducesResponseType(400)]

        public async Task<ActionResult<List<ClubDto>>> GetClubsByLocation(int locationId)
        {
            var clubs = await _locationRepository.GetClubsByLocation(locationId);
            if (clubs == null)
                return NotFound("No clubs currently available in that location");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var clubsToReturn = _mapper.Map<List<ClubDto>>(clubs);

            return Ok(clubsToReturn);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]

        public async Task<ActionResult<bool>> CreateLocation(
           [FromBody] LocationDto locationCreate)
        {
            if (locationCreate == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var locationMap = _mapper.Map<Location>(locationCreate);

            return Ok("Sucessfully added new location to records");
        }


    }

}