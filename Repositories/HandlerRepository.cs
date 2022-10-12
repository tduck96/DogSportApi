using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;
using RealPetApi.Dtos;
using RealPetApi.Models;

namespace RealPetApi.Repositories
{
    public class HandlerRepository : IHandlerRepository
    {
        private readonly DataContext _context;

        public HandlerRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<bool>CreateHandler(Handler handlerToCreate)
        {
            await _context.Handlers.AddAsync(handlerToCreate);
            var created = await _context.SaveChangesAsync();
            return created > 0;

        }

        public async Task<bool> DeleteHandler(int handlerId)
        {
            var handler = await GetHandler(handlerId);

            _context.Handlers.Remove(handler);

            var deleted = await _context.SaveChangesAsync();

            return true;
        }

        public async Task<List<Dog>> GetDogsByHandler(int handlerId)
        {
            return await _context.Dogs.Where(c => c.HandlerId == handlerId).ToListAsync();
            
        }

        public async Task<Handler> GetHandler(int id)
        {
            return await _context.Handlers
                .Include(c => c.Dogs)
                .FirstOrDefaultAsync(h => h.Id == id);
        }

        public async Task<List<HandlersListDto>> GetHandlers()
        {
            var handlers = await _context.Handlers.ToListAsync();
            List<HandlersListDto> Dtos = new List<HandlersListDto>(); 

            foreach (Handler handler  in handlers)
            {
               
                var location = await _context.Locations.Where(c => c.Id == handler.LocationId).Select(c => c.Name).FirstOrDefaultAsync();
                

                var dto = new HandlersListDto
                {
                Id = handler.Id,
                Name = handler.Name,
                PhotoUrl = handler.PhotoUrl,
                Location = location
                };

                Dtos.Add(dto);
            }
            return Dtos;
        }



        public async Task<bool> UpdateHandler(Handler handlerToUpdate)
        {
            _context.Handlers.Update(handlerToUpdate);
            var updated = await _context.SaveChangesAsync();
            return updated > 0;
        }

        public async Task<List<PhotoDto>> GetPhotosByHandler(int handlerId)
        {
            var photos = await _context.Photos.Where(c => c.HandlerId == handlerId).ToListAsync();

                List<PhotoDto> photoDtos = new List<PhotoDto>();

                foreach (Photo photo in photos)
                {
                    var dto = new PhotoDto
                    {
                        Id = photo.Id,
                        Url = photo.Url,
                        PublicId = photo.PublicId
                    };

                    photoDtos.Add(dto);
                }

                return photoDtos;
        }
           
    }
}

