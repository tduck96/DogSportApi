using System;
using RealPetApi.Models;

namespace RealPetApi.Repositories
{
    public class SportRepository : ISportRepository
    {
        private readonly DataContext _context;

        public SportRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<ICollection<Sport>> GetSports()
        {
            return await _context.Sports.ToListAsync();
        }

        public async Task<Sport> GetSport(int id)
        {
            return await _context.Sports.FirstOrDefaultAsync(s => s.Id == id);
        }


        public async Task<ICollection<Dog>> GetDogsBySport(int sportId)
        {
            return await _context.DogSports.Where(e => e.SportId == sportId).Select(c => c.Dog).ToListAsync();
        }

        public async Task<ICollection<Club>> GetClubsBySport(int sportId)
        {
            return await _context.ClubSports.Where(c => c.SportId == sportId).Select(c => c.Club).ToListAsync();
        }

        public async Task<bool> CreateSport(Sport sportToCreate)
        {
            await _context.Sports.AddAsync(sportToCreate);
            var created = await _context.SaveChangesAsync();
            return created > 0;
        }

        public async Task<bool> UpdateSport(Sport sportToUpdate)
        {
            _context.Sports.Update(sportToUpdate);
            var updated = await _context.SaveChangesAsync();
            return updated > 0;
        }

        public async Task<bool> DeleteSport(int sportId)
        {
            var location = await GetSport(sportId);
            _context.Sports.Remove(location);
            var deleted = await _context.SaveChangesAsync();
            return true;
        }

        public async Task<ICollection<UserProfile>> GetUsersBySport(int sportId)
        {
            return await _context.HandlerSports.Where(e => e.SportId == sportId).Select(c => c.UserProfile).ToListAsync();
        }
    }
}

