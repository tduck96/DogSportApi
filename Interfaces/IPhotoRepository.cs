using System;
using RealPetApi.Models;

namespace RealPetApi.Interfaces
{
    public interface IPhotoRepository
    {
        Task<Photo> GetPhoto(int photoId);
        Task<bool> AddPhoto(Photo photoToAdd);
        Task<bool> DeletePhoto(int photoId);
    }
}

