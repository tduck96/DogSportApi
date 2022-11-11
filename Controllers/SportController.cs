using System;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RealPetApi.Dtos;
using RealPetApi.Models;

namespace RealPetApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class SportController : Controller
    {
        private readonly ISportRepository _sportRepository;
        private readonly IMapper _mapper;

        public SportController(ISportRepository sportRepository,
            IMapper mapper)
        {
            _sportRepository = sportRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Sport>))]

        public async Task<ActionResult<List<SportDto>>> GetSports()
        {
            //var sports = _mapper.Map<List<SportDto>>(_sportRepository.GetSports());
            var sports = await _sportRepository.GetSports();

            var sportsToReturn = _mapper.Map<List<SportDto>>(sports);

            return Ok(sportsToReturn);


        }

            [HttpGet("{sportId}")]
            [ProducesResponseType(200, Type = typeof(Sport))]
            [ProducesResponseType(400)]

            public async Task<ActionResult<SportDto>> GetSport(int sportId)
            {
                var sport = await _sportRepository.GetSport(sportId);
                if (sport == null)
                    return NotFound();
    

                var sportToReturn = _mapper.Map<SportDto>(sport);

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                return Ok(sportToReturn);
            }

            [HttpGet("clubs/{sportId}")]
            [ProducesResponseType(200, Type = typeof(Club))]
            [ProducesResponseType(400)]

            public async Task <ActionResult<List<ClubDto>>> GetClubsBySport(int sportId)
            {
                var clubs = await _sportRepository.GetClubsBySport(sportId);
            
                 var clubsToReturn = _mapper.Map<List<ClubDto>>(clubs);

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                return Ok(clubsToReturn);
            }

        [HttpGet("user/{sportId}")]
        [ProducesResponseType(200, Type = typeof(Club))]
        [ProducesResponseType(400)]

        public async Task<ActionResult<List<ClubDto>>> GetUsersBySport(int sportId)
        {
            var users = await _sportRepository.GetUsersBySport(sportId);

            var usersToReturn = _mapper.Map<List<UserListDto>>(users);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(usersToReturn);
        }



        [HttpGet("dogs/{sportId}")]
            [ProducesResponseType(200, Type = typeof(Dog))]
            [ProducesResponseType(400)]

            public async Task<ActionResult<List<DogDto>>> GetDogsBySport(int sportId)
            {
                var dogs = await _sportRepository.GetDogsBySport(sportId);

                var dogsToReturn = _mapper.Map<List<DogDto>>(dogs);

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                return Ok(dogsToReturn);
            }

            [HttpPost]
            [ProducesResponseType(204)]
            [ProducesResponseType(400)]

            public async Task<ActionResult<bool>> CreateSport(
             [FromBody] SportDto sportCreate)
            {
            if (sportCreate == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var sportMap = _mapper.Map<Sport>(sportCreate);
            sportMap.PhotoUrl = "https://res.cloudinary.com/dx58mbwcg/image/upload/v1668129584/Screen_Shot_2022-11-10_at_7.18.31_PM_imj6c6.png";

            await _sportRepository.CreateSport(sportMap);

            return Ok("Sucessfully added new sport to records");
        }


    }
}
