using System;
using RealPetApi.Dtos;
using RealPetApi.Interfaces;
using RealPetApi.Models;

namespace RealPetApi.Repositories
{
    public class ClubRepository : IClubRepository
    {
        private readonly DataContext _context;

        public ClubRepository(DataContext context)
        {
            _context = context;
        }

        public bool ClubExists(int id)
        {
            return _context.Clubs.Any(c => c.Id == id);
        }


        public async Task<bool> Save()
        {
            var saved = await _context.SaveChangesAsync();
            return saved > 0 ? true : false;
        }

        public async Task<bool> CreateClub(Club clubToCreate)
        {
            await _context.Clubs.AddAsync(clubToCreate);

            var created = await _context.SaveChangesAsync();
            return created > 0;
        }

        public async Task<bool> DeleteClub(int clubId)
        {
            var club = await GetClub(clubId);

            _context.Clubs.Remove(club);

            var deleted = await _context.SaveChangesAsync();

            return true; 
        }

        public async Task<Club> GetClub(int id)
        {
            return await _context.Clubs.FirstOrDefaultAsync(c => c.Id == id);      
        }

        public async Task<IEnumerable<Club>> GetClubs()
        {
            return await _context.Clubs.ToListAsync();
        }

        public async  Task<bool> UpdateClub(Club clubToUpdate)
        {
            _context.Clubs.Update(clubToUpdate);

            var updated = await _context.SaveChangesAsync();

            return updated > 0;
        }

        public async Task<List<Sport>> GetSportsByClub(int clubId)
        {
            return await _context.ClubSports.Where(e => e.ClubId == clubId).Select(c => c.Sport).ToListAsync();


        }
    }
}

