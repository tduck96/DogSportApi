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

        public IActionResult GetLocations()
        {
            var locations = _mapper.Map<List<LocationDto>>(_locationRepository.GetLocations());


            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(locations);
        }

        [HttpGet("{locationId}")]
        [ProducesResponseType(200, Type = typeof(Handler))]
        [ProducesResponseType(400)]

        public IActionResult GetLocation(int locationId)
        {
            if (!_locationRepository.LocationExists(locationId))
                return NotFound();

            var location = _mapper.Map<HandlerDto>(_locationRepository.GetLocation(locationId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(location);
        }

        [HttpGet("clubs/{locationId}")]
        [ProducesResponseType(200, Type = typeof(Club))]
        [ProducesResponseType(400)]

        public IActionResult GetClubsByLocation(int locationId)
        {
            if (!_locationRepository.LocationExists(locationId))
                return NotFound();

            var clubs = _mapper.Map<List<ClubDto>>(
                _locationRepository.GetClubsByLocation(locationId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(clubs);
        }
    }
}

