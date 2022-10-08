using System;
using RealPetApi.Dtos;
using RealPetApi.Models;

namespace RealPetApi.Interfaces
{
    public interface IProfileRepository
    {
        Task<HandlerProfileDto> GetProfile(int handlerId);
        Task<bool> UpdateProfile(int handlerId);
        Task<List<WallPost>> GetWallPostsByProfile(int handlerId);
    }
}

