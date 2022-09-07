using System;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RealPetApi.Dtos;
using RealPetApi.Models;

namespace RealPetApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class HandlerController : Controller
    {
        private readonly IHandlerRepository _handlerRepository;
        private readonly IMapper _mapper;
        private readonly ILocationRepository _locationRepository;

        public HandlerController(IHandlerRepository handlerRepository,
         IMapper mapper,
         ILocationRepository locationRepository)
        {
            _handlerRepository = handlerRepository;
            _mapper = mapper;
           _locationRepository = locationRepository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Handler>))]

        public IActionResult GetHandlers()
        {
            var handlers = _mapper.Map<List<HandlerDto>>(_handlerRepository.GetHandlers());


            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(handlers);
        }

        [HttpGet("{handlerId}")]
        [ProducesResponseType(200, Type = typeof(Handler))]
        [ProducesResponseType(400)]

        public IActionResult GetDog(int handlerId)
        {
            if (!_handlerRepository.HandlerExists(handlerId))
                return NotFound();

            var handler = _mapper.Map<HandlerDto>(_handlerRepository.GetHandler(handlerId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(handler);
        }


        [HttpGet("dogs/{handlerId}")]
        [ProducesResponseType(200, Type = typeof(Dog))]
        [ProducesResponseType(400)]

        public IActionResult GetDogsByHandler(int handlerId)
        {
            if (!_handlerRepository.HandlerExists(handlerId))
                return NotFound();

            var dogs = _mapper.Map<List<DogDto>>(
                _handlerRepository.GetDogsByHandler(handlerId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(dogs);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]

        public IActionResult CreateHandler([FromQuery] int locationId,
            [FromBody] HandlerDto handlerCreate)
        {
            if (handlerCreate == null)
                return BadRequest(ModelState);

            var handler = _handlerRepository.GetHandlers()
                 .Where(b => b.Name.Trim().ToUpper() == handlerCreate.Name.TrimEnd().ToUpper())
                 .FirstOrDefault();

            if (handler != null)
            {
                ModelState.AddModelError("", "Breed already exits.");
                return StatusCode(422, ModelState);
            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var handlerMap = _mapper.Map<Handler>(handlerCreate);


            handlerMap.Location = _locationRepository.GetLocation(locationId);
           


            if (!_handlerRepository.CreateHandler(handlerMap))
            {
                ModelState.AddModelError("", "Something went wrong with save");
                return StatusCode(500, ModelState);
            }
            return Ok("Sucessfully added new handler to records");
        }

        [HttpPut("{handlerId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateDog(int handlerId, [FromBody] HandlerDto updatedHandler)
        {
            if (updatedHandler == null)
                return BadRequest(ModelState);

            if (handlerId != updatedHandler.Id)
                return BadRequest(ModelState);

            if (!_handlerRepository.HandlerExists(handlerId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var handlerMap = _mapper.Map<Handler>(updatedHandler);

            if (!_handlerRepository.UpdateHandler(handlerMap))
            {
                ModelState.AddModelError("", "Something went wrong updating record");
                return StatusCode(500, ModelState);

            }
            return NoContent();
        }

        [HttpDelete("{handlerId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]

        public IActionResult DeleteHandler(int handlerId)
        {
            if (!_handlerRepository.HandlerExists(handlerId))
            {
                return NotFound();
            }

            var handlerToDelete = _handlerRepository.GetHandler(handlerId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_handlerRepository.DeleteHandler(handlerToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting category");

            }
            return NoContent();
        }
    }
}

