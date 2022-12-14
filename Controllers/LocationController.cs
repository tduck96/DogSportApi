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
        [ProducesResponseType(200, Type = typeof(IEnumerable<LocationDto>))]

        public async Task<ActionResult<List<LocationDto>>> GetLocations()
        {
            var locations = await _locationRepository.GetLocations();
            var locationsToReturn = _mapper.Map<List<LocationDto>>(locations);

            return Ok(locationsToReturn);
        }

        [HttpGet("{locationId}")]
        [ProducesResponseType(200, Type = typeof(LocationDto))]
        [ProducesResponseType(400)]

        public async Task<ActionResult> GetLocation(int locationId)
        {
            var location = await _locationRepository.GetLocation(locationId);
            if (location == null)
                return NotFound();

            var locationToReturn = _mapper.Map<LocationDto>(location);    

            return Ok(locationToReturn);
        }

        [HttpGet("clubs/{locationId}")]
        [ProducesResponseType(200, Type = typeof(ClubDto))]
        [ProducesResponseType(400)]

        public async Task<ActionResult<List<ClubDto>>> GetClubsByLocation(int locationId)
        {
            var clubs = await _locationRepository.GetClubsByLocation(locationId);
            if (clubs == null)
                return NotFound("No clubs currently available in that location");

            return Ok(clubs);
        }

        [HttpGet("handlers/{locationId}")]
        [ProducesResponseType(200, Type = typeof(UserListDto))]
        [ProducesResponseType(400)]

        public async Task<ActionResult<List<UserListDto>>> GetUsersByLocation(int locationId)
        {
            var users = await _locationRepository.GetUsersByLocation(locationId);

            if (users == null)
                return NotFound("No users currently available in that location");

            var usersToReturn = _mapper.Map<List<UserListDto>>(users);

            return Ok(usersToReturn);
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

            await _locationRepository.CreateLocation(locationMap);

            return Ok("Sucessfully added new location to records");
        }


    }

}