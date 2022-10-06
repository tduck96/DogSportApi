using System;
using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;
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

        public async Task<ICollection<Handler>> GetHandlers()
        {
            return await _context.Handlers.ToListAsync();
        }

        public async Task<bool> UpdateHandler(Handler handlerToUpdate)
        {
            _context.Handlers.Update(handlerToUpdate);
            var updated = await _context.SaveChangesAsync();
            return updated > 0;
        }
    }
}

