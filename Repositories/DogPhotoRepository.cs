using System;
using RealPetApi.Models;

namespace RealPetApi.Repositories
{
    public class DogPhotoRepository : IDogPhotoRepository
    {
        private readonly DataContext _context;

        public DogPhotoRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<bool> AddPhoto(DogPhoto photoToAdd)
        {
            await _context.DogPhotos.AddAsync(photoToAdd);
            var created = await _context.SaveChangesAsync();
            return created > 0;
        }

        public Task<bool> DeletePhoto(int photoId)
        {
            throw new NotImplementedException();
        }

        public async Task<DogPhoto> GetPhoto(int photoId)
        {
            return await _context.DogPhotos.Where(c => c.Id == photoId).FirstOrDefaultAsync();
        }
    }
}
