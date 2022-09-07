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

        public HandlerController(IHandlerRepository handlerRepository,
         IMapper mapper)
        {
            _handlerRepository = handlerRepository;
            _mapper = mapper;
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
    }
}

