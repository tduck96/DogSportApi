﻿using System;
using System.Security.Cryptography;
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
        private readonly IBreedRepository _breedRepository;

        public HandlerController(IHandlerRepository handlerRepository,
         IMapper mapper,
         ILocationRepository locationRepository,
         IBreedRepository breedRepository
         )
        {
            _handlerRepository = handlerRepository;
            _mapper = mapper;
           _locationRepository = locationRepository;
            _breedRepository = breedRepository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Handler>))]

        public async Task<ActionResult<HandlerDto>> GetHandlers()
        {
            var handlers = await _handlerRepository.GetHandlers();

            var handlersToReturn = _mapper.Map<List<HandlerDto>>(handlers);


            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(handlersToReturn);
        }

        [HttpGet("{handlerId}")]
        [ProducesResponseType(200, Type = typeof(Handler))]
        [ProducesResponseType(400)]

        public async Task<ActionResult<HandlerDto>> GetHandler(int handlerId)
        {
            var handler = await _handlerRepository.GetHandler(handlerId);

            if (handler != null)
            {
                var handlerToReturn = _mapper.Map<HandlerDto>(handler);
                return Ok(handlerToReturn);
            }
                return NotFound();
            
           
        }


        [HttpGet("dogs/{handlerId}")]
        [ProducesResponseType(200, Type = typeof(Dog))]
        [ProducesResponseType(400)]

        public async Task<ActionResult<DogDto>> GetDogsByHandler(int handlerId)
        {
            var dogs = await _handlerRepository.GetDogsByHandler(handlerId);
           
            var dogsToReturn = _mapper.Map<List<DogDto>>(dogs);
           
            return Ok(dogsToReturn);

            
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]

        public async Task<ActionResult> CreateHandler([FromQuery] int locationId,
            [FromBody] HandlerDto handlerCreate)
        {
            var handlerMap = _mapper.Map<Handler>(handlerCreate);


            handlerMap.Location = _locationRepository.GetLocation(locationId);
            

            await _handlerRepository.CreateHandler(handlerMap);

            return Ok("Sucessfully added new dog to records");
        }

        [HttpPatch("{handlerId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateHandler(int handlerId, [FromBody] HandlerDto updatedHandler)
        {
       
            return NoContent();
        }

        //[HttpDelete("{handlerId}")]
        //[ProducesResponseType(400)]
        //[ProducesResponseType(204)]
        //[ProducesResponseType(404)]

        //public IActionResult DeleteHandler(int handlerId)
        //{
        //    if (!_handlerRepository.HandlerExists(handlerId))
        //    {
        //        return NotFound();
        //    }

        //    var handlerToDelete = _handlerRepository.GetHandler(handlerId);

        //    if (!ModelState.IsValid)
        //        return BadRequest(ModelState);

        //    if (!_handlerRepository.DeleteHandler(handlerToDelete))
        //    {
        //        ModelState.AddModelError("", "Something went wrong deleting category");

        //    }
        //    return NoContent();
        //}
    }
}

