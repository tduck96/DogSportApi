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

        [HttpPost("login")]
        public async Task<ActionResult<Handler>> LoginUser(AuthHandlerDto request)
        {
            var response = await _authService.LoginUser(request);
            if (response.Success)
                return Ok(response);

            return BadRequest(response.Message);
        }

        [HttpPost("refresh-token")]
        public async Task<ActionResult<string>> RefreshToken()
        {
            var response = await _authService.RefreshToken();
            if (response.Success)
                return Ok(response);

            return BadRequest(response.Message);
        }

        [HttpPost("logout")]
        public async Task<ActionResult<string>> LogoutUser()
        {
            var response = await _authService.LogoutUser();
            if (response.Success)
                return Ok(response);

            return BadRequest(response.Message);
        }
    }
}

