using System;
using RealPetApi.Models;

namespace RealPetApi.Repositories
{
    public class PhotoRepository : IPhotoRepository
    {
        private readonly DataContext _context;

        public PhotoRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<bool> AddPhoto(Photo photoToAdd)
        {
            await _context.Photos.AddAsync(photoToAdd);
            var created = await _context.SaveChangesAsync();
            return created > 0;
        }

        public async Task<bool> UpdatePhoto(Photo photoToAdd)
        {
            _context.Photos.Update(photoToAdd);
            var updated = await _context.SaveChangesAsync();
            return updated > 0;
        }

        public Task<bool> DeletePhoto(int photoId)
        {
            throw new NotImplementedException();
        }

        public async Task<Photo> GetPhoto(int photoId)
        {
            return await _context.Photos.Where(c => c.Id == photoId).FirstOrDefaultAsync();
        }
    }
}

