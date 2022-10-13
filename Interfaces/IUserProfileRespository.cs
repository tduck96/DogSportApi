using System;
using RealPetApi.Dtos;
using RealPetApi.Models;

namespace RealPetApi.Interfaces
{
    public interface IUserProfileRespository
    {
        Task<UserProfile> GetUser(int userId);
        Task<List<UserProfile>> GetUsers();
        Task<bool> UpdateUserInfo(UserProfile user);
        Task<bool> CreateUser(UserProfile user);

        Task<Handler> GetUserHandler(int userId);
        Task <List<Dog>> GetDogsByUser(int userId);
        Task <List<PhotoDto>> GetPhotosByUser(int userId);
    }
}

