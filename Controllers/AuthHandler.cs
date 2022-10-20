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
    public class AuthHandler : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IMapper _mapper;
        private readonly IUserProfileRespository _userProfileRepository;

        public AuthHandler(IAuthService authService,
            IMapper mapper,
            IUserProfileRespository userProfileRepository)
        {
            _authService = authService;
            _mapper = mapper;
            _userProfileRepository = userProfileRepository;
        }

        [HttpPost]
        public async Task<ActionResult<Handler>> RegisterUser(AuthHandlerDto request)
        {
            var response = await _authService.RegisterUser(request);
            var responseToSend = _mapper.Map<AuthResponseDto>(response);

            

            return Ok(responseToSend);
        }

        [HttpPost("login")]
        public async Task<ActionResult<Handler>> LoginUser(AuthHandlerDto request)
        {
            var response = await _authService.LoginUser(request);

                return Ok(response);

            

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

