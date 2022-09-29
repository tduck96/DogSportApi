using System;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RealPetApi.Dtos;
using RealPetApi.Interfaces;
using RealPetApi.Models;

namespace RealPetApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class BreedController : Controller
    {
        private readonly IBreedRepository _breedRepository;
        private readonly IMapper _mapper;

        public BreedController(IBreedRepository breedRepository,
            IMapper mapper)
        {
            _breedRepository = breedRepository;
            _mapper = mapper;
        }


        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Breed>))]

        public async Task<ActionResult<BreedDto>> GetBreeds()
        {
            var breeds = await _breedRepository.GetBreeds();
            var breedstoReturn = _mapper.Map<List<BreedDto>>(breeds);
            

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(breedstoReturn);
        }


        [HttpGet("{breedId}")]
        [ProducesResponseType(200, Type = typeof(Breed))]
        [ProducesResponseType(400)]

        public async Task<ActionResult<BreedDto>> GetBreed(int breedId)
        {
            var breed = await _breedRepository.GetBreed(breedId);
            var breedToReturn = _mapper.Map<BreedDto>(breed);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(breedToReturn);

        }

  

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]

        public async Task <ActionResult<Breed>> CreateBreed(BreedDto breedCreate)
        {
        
            var newbreed = await _breedRepository.GetBreeds();

            var checkBreed = newbreed?.Where(b => b.Name.Trim().ToUpper() == breedCreate.Name.TrimEnd().ToUpper())
         .FirstOrDefault();

            if (checkBreed == null)
            {

               var breed = await _breedRepository.CreateBreed(breedCreate);
            }

            return Ok("Sucessfully added");
                
            
        }


        [HttpPut("{breedId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]

        public async Task<ActionResult> UpdateBreed([FromBody] Breed request)
        {
            var userId = request.Id;

            var breed = new Breed
            {
                Id = userId,
                Name = request.Name
            };

            var updated = await _breedRepository.UpdateBreed(breed);

            if (updated)
                return Ok();

            return NotFound();
        }


        [HttpDelete("{breedId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]

        public async Task<ActionResult> DeleteBreed([FromBody] int breedId)
        {
            var toDelete = await _breedRepository.DeleteBreed(breedId);

            if (toDelete == true)
                return Ok("Successfully Removed");

            return NotFound();
        }

    }
}

