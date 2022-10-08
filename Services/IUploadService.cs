using System;
using CloudinaryDotNet.Actions;

namespace RealPetApi.Services
{
    public interface IUploadService
    {
        Task<ImageUploadResult> AddPhotoAsync(IFormFile file);
        Task<DeletionResult> DeletePhotoAsync(string publicId);
    }
}

