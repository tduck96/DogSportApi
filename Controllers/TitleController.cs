using System;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RealPetApi.Dtos;
using RealPetApi.Models;

namespace RealPetApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class TitleController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ITitleRepository _titleRepository;

        public TitleController(ITitleRepository titleRepository,
            IMapper mapper)
        {
            _titleRepository = titleRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<TitleDto>))]

        public async Task<ActionResult<List<TitleDto>>> GetTitles()
        {
 
            var titles = await _titleRepository.GetTitles();

            var titlesToReturn = _mapper.Map<List<TitleDto>>(titles);

            return Ok(titlesToReturn);


        }


        [HttpGet("{titleId}")]
        [ProducesResponseType(200, Type = typeof(TitleDto))]
        [ProducesResponseType(400)]

        public async Task<ActionResult<TitleDto>> GetTitle(int titleId)
        {
            var title = await _titleRepository.GetTitle(titleId);
            if (title == null)
                return NotFound();


            var titleToReturn = _mapper.Map<TitleDto>(title);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(titleToReturn);

        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]

        public async Task<ActionResult<bool>> CreateTitle(
             [FromBody] TitleDto titleCreate)
        {
            if (titleCreate == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var titleMap = _mapper.Map<Title>(titleCreate);

            await _titleRepository.CreateTitle(titleMap);

            return Ok("Sucessfully added new Title to records");
        }


    }
}

