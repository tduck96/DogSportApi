using System;
using RealPetApi.Dtos;
using RealPetApi.Models;

namespace RealPetApi.Interfaces
{
    public interface IWallPostRepository
    {
        Task<List<WallPostDto>> GetWallPosts();
        Task<WallPost> GetWallPost(int wallpostId);
        Task<ICollection<Comment>> GetWallPostComments(int wallpostId);
        Task<bool> CreateWallPost(WallPost wallPostCreate);
        Task<bool> UpdateWallPost(WallPost wallPostUpdate);
        Task<bool> DeleteWallPost(int wallpostId);
    }
}

