using System;
using RealPetApi.Dtos;
using RealPetApi.Models;

namespace RealPetApi.Services
{
    public interface IAuthService
    {
        Task<Handler> RegisterUser(AuthHandlerDto request);
        Task<AuthResponseDto> LoginUser(AuthHandlerDto request);
        Task<AuthResponseDto> RefreshToken();
        Task<AuthResponseDto> LogoutUser();

    }
}

