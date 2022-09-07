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

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]

        public IActionResult CreateLocation(
           [FromBody] LocationDto locationCreate)
        {
            if (locationCreate == null)
                return BadRequest(ModelState);

            var location = _locationRepository.GetLocations()
                 .Where(b => b.Name.Trim().ToUpper() == locationCreate.Name.TrimEnd().ToUpper())
                 .FirstOrDefault();

            if (location!= null)
            {
                ModelState.AddModelError("", "Breed already exits.");
                return StatusCode(422, ModelState);
            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var locationMap = _mapper.Map<Location>(locationCreate);



            if (!_locationRepository.CreateLocation(locationMap))
            {
                ModelState.AddModelError("", "Something went wrong with save");
                return StatusCode(500, ModelState);
            }
            return Ok("Sucessfully added new location to records");
        }

        [HttpPut("{locationId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateLocation(int locationId, [FromBody] LocationDto updatedLocation)
        {
            if (updatedLocation == null)
                return BadRequest(ModelState);

            if (locationId != updatedLocation.Id)
                return BadRequest(ModelState);

            if (!_locationRepository.LocationExists(locationId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var locationMap = _mapper.Map<Location>(updatedLocation);

            if (!_locationRepository.UpdateLocation(locationMap))
            {
                ModelState.AddModelError("", "Something went wrong updating record");
                return StatusCode(500, ModelState);

            }
            return NoContent();
        }

        [HttpDelete("{locationId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]

        public IActionResult DeleteBreed(int locationId)
        {
            if (!_locationRepository.LocationExists(locationId))
            {
                return NotFound();
            }

            var locationToDelete = _locationRepository.GetLocation(locationId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_locationRepository.DeleteLocation(locationToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting category");

            }
            return NoContent();
        }
    }
}

