using System;
using Microsoft.AspNetCore.Mvc;
using RealPetApi.Dtos;
using RealPetApi.Models;
using RealPetApi.Services;

namespace RealPetApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthHandler : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthHandler(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        public async Task<ActionResult<Handler>> RegisterUser(AuthHandlerDto request)
        {
            var response = await _authService.RegisterUser(request);
            return Ok(response);
        }
    }
}

