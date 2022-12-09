using System;
using RealPetApi.Dtos;
using RealPetApi.Models;

namespace RealPetApi.Interfaces
{
    public interface IUserProfileRespository
    {
        Task<UserProfile> GetUser(int userId);
        Task<IEnumerable<UserListDto>> GetUsers();
        Task<bool> UpdateUserInfo(UserProfile user);
        Task<bool> CreateUser(UserProfile user);
        Task<bool> AddUserSport(HandlerSport sport);
        Task<bool> RemoveUserSport(HandlerSport sport);
        Task<List<Sport>> GetSportsByUser(int userId);
        Task<HandlerSport> GetHandlerSport(int userId, int sportId);
        Task<Handler> GetUserHandler(int userId);
        Task<List<DogDtoForUserProfile>> GetDogsByUser(int userId);
        Task<List<WallPostProfileDto>> GetWallPostsByUser(int userId);
        Task<List<PhotoDto>> GetPhotosByUser(int userId);

        Task<UserFollowing> GetFollower(int userId, int followId);
        Task<bool> FollowUser(UserFollowing user);
        Task<bool> UnfollowUser(int userId, int followId);
        Task<bool> FollowChecker(int userId, int followingId);
        Task<List<UserProfile>> GetUserFollowing(int userId);
    }
}

