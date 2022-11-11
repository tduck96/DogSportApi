using System;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RealPetApi.Dtos;
using RealPetApi.Models;
using RealPetApi.Services;

namespace RealPetApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IUploadService _uploadService;
        private readonly IPhotoRepository _photoRepository;

        public ProfileController(
         IMapper mapper,
         IUploadService uploadService)

        {
            _mapper = mapper;
            _uploadService = uploadService;
        
        }

      
        [HttpPost("addProfilepic/")]

        public async Task<ActionResult<bool>> UploadPhoto(IFormFile file)
        {
           
            var result = await _uploadService.AddPhotoAsync(file);

            if (result.Error != null) return BadRequest(result.Error.Message);

          
                var photo = new Photo
                {
                    Url = result.SecureUrl.AbsoluteUri,
                    PublicId = result.PublicId,

                };


                return Ok(photo.Url);
            }

       





    }
}

