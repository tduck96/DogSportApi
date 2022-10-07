using System;
using System.Security.Cryptography;
using RealPetApi.Models;

namespace RealPetApi.Repositories
{
    public class TitleRepository : ITitleRepository
    {
        private readonly DataContext _context;

        public TitleRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateTitle(Title titleToCreate)
        {
            await _context.Titles.AddAsync(titleToCreate);
            var created = await _context.SaveChangesAsync();
            return created > 0;
        }

        public async Task<bool> DeleteTitle(int titleId)
        {
            var title = await GetTitle(titleId);
            _context.Titles.Remove(title);
            var deleted = await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Title> GetTitle(int titleId)
        {
            return await _context.Titles.FirstOrDefaultAsync(t => t.Id == titleId);
        }

        public async Task<List<Title>> GetTitles()
        {
            return await _context.Titles.ToListAsync();
        }

        public async Task<bool> UpdateTitle(Title titleToUpdate)
        {
            _context.Titles.Update(titleToUpdate);
            var updated = await _context.SaveChangesAsync();
            return updated > 0;
        }
    }
}

