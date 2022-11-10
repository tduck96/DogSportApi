using System;
using RealPetApi.Dtos;
using RealPetApi.Models;

namespace RealPetApi.Interfaces
{
    public interface IUserProfileRespository
    {
        Task<UserProfile> GetUser(int userId);
        Task<List<UserListDto>> GetUsers();
        Task<bool> UpdateUserInfo(UserProfile user);
        Task<bool> CreateUser(UserProfile user);
        Task<bool> AddUserSport(HandlerSport sport);
        Task<List<Sport>> GetSportsByUser(int userId);
        Task<Handler> GetUserHandler(int userId);
        Task<List<DogDtoForUserProfile>> GetDogsByUser(int userId);
        Task<List<WallPostProfileDto>> GetWallPostsByUser(int userId);
        Task<List<PhotoDto>> GetPhotosByUser(int userId);
    }
}

