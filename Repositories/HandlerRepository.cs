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

     
        public async Task<Handler> GetHandler(int id)
        {
            return await _context.Handlers.Where(c => c.Id == id).FirstOrDefaultAsync(h => h.Id == id);
        }

        public Task<List<HandlersListDto>> GetHandlers()
        {
            throw new NotImplementedException();
        }

        public async Task<UserProfile> GetUserProfile(int handlerId)
        {
            return await _context.UserProfiles.Where(c => c.HandlerId == handlerId).Include(c => c.Location).FirstOrDefaultAsync();
        }

        public async Task<bool> UpdateHandler(Handler handlerToUpdate)
        {
            _context.Handlers.Update(handlerToUpdate);
            var updated = await _context.SaveChangesAsync();
            return updated > 0;
        }

      
           
    }
}

