using System;
using RealPetApi.Models;

namespace RealPetApi.Interfaces
{
    public interface IDogPhotoRepository
    {
        Task<DogPhoto> GetPhoto(int photoId);
        Task<bool> AddPhoto(DogPhoto photoToAdd);
        Task<bool> DeletePhoto(int photoId);
    }
}

