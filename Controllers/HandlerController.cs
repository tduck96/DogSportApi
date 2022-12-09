using System;
using System.Security.Cryptography;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
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
        [ProducesResponseType(200, Type = typeof(IEnumerable<HandlersListDto>))]

        public async Task<ActionResult<List<HandlersListDto>>> GetHandlers()
        {
            var handlers = await _handlerRepository.GetHandlers();

            return Ok(handlers);
        }

        [HttpGet("{handlerId}")]
        [ProducesResponseType(200, Type = typeof(HandlerDto))]
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



        [HttpPut("{handlerId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> UpdateHandler(int handlerId, [FromBody] HandlerUpdateDto handlerCreate)
        {
            var handler = await _handlerRepository.GetHandler(handlerId);

            var handlerMap = _mapper.Map<Handler>(handlerCreate);

            if (handler == null)
                return NotFound();

           
            await _handlerRepository.UpdateHandler(handlerMap);

            return Ok(handlerMap);



        }

        [HttpDelete("{handlerId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]

        public async Task<ActionResult<bool>> DeleteHandler(int handlerId)
        {
            var deleted = await _handlerRepository.DeleteHandler(handlerId);
            if (deleted)
                return Ok("Handler deleted");

          
                return NotFound();

        }
    }
}

