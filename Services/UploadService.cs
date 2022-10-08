using System;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.Extensions.Options;

namespace RealPetApi.Services
{
    public class UploadService : IUploadService
    {

    private readonly Cloudinary _cloudinary;
    private readonly DataContext _context;


        public UploadService(IConfiguration config, DataContext context)
        {
            var CloudName = config.GetSection("AppSettings: CloudName").Value;
            var Key = config.GetSection("AppSettings: Key").Value;
            var Secret = config.GetSection("AppSettings: Sec").Value;

            Account account = new Account(
                 "dx58mbwcg",
                  "343199191951211",
                    "_15L3d5PhWMbkwjQse1zltD5Hw0");
                
            _cloudinary = new Cloudinary(account);
            _context = context;
        }

        public async Task<ImageUploadResult> AddPhotoAsync(IFormFile file)
        {
            var uploadResult = new ImageUploadResult();

            if (file.Length > 0)
            {
                await using var stream = file.OpenReadStream();
                var uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(file.FileName, stream),
                    Transformation = new Transformation().Height(500).Width(500).Crop("fill").Gravity("face")
                };
                uploadResult = await _cloudinary.UploadAsync(uploadParams);
            }

            return uploadResult;
        }

        public async Task<DeletionResult> DeletePhotoAsync(string publicId)
        {
            var deleteParams = new DeletionParams(publicId);
            var result = await _cloudinary.DestroyAsync(deleteParams);

            return result;
        }
    }
      
}

