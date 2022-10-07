using System;
using RealPetApi.Models;

namespace RealPetApi.Interfaces
{
    public interface ICommentRepository
    {
        Task<Comment> GetComment(int commentId);
        Task<bool> UpdateComment(Comment commentToUpdate);
        Task<bool> CreateComment(Comment commentToUpdate);
        Task<bool> DeleteComment(int commentId);
    }
}

