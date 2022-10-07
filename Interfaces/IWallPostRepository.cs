using System;
using RealPetApi.Models;

namespace RealPetApi.Interfaces
{
    public interface IWallPostRepository
    {
        Task<List<WallPost>> GetWallPosts();
        Task<WallPost> GetWallPost(int wallpostId);
        Task<List<Comment>> GetWallPostComments(int wallpostId);
        Task<bool> CreateWallPost(WallPost wallPostCreate);
        Task<bool> UpdateWallPost(WallPost wallPostUpdate);
        Task<bool> DeleteWallPost(int wallpostId);
    }
}

