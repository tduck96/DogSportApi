using System;
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

        public Club GetClub(int id)
        {
            return _context.Clubs.Where(c => c.Id == id).FirstOrDefault();
        }

        public ICollection<Club> GetClubs()
        {
            return _context.Clubs.ToList();
        }

        public ICollection<Sport> GetSportsByClub(int clubId)
        {
            return _context.ClubSports.Where(c => c.ClubId == clubId).Select(c => c.Sport).ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}

