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

        public ICollection<Club> GetClubsBySport(int sportId)
        {
            return _context.ClubSports.Where(s => s.SportId == sportId).Select(c => c.Club).ToList();
        }

        public ICollection<Handler> GetHandlersBySport(int sportId)
        {
            return _context.HandlerSports.Where(s => s.SportId == sportId).Select(s => s.Handler).ToList();
        }

        public ICollection<Sport> GetSports()
        {
            return _context.Sports.ToList();
        }

        public Sport GetSport(int id)
        {
            return _context.Sports.Where(s => s.Id == id).FirstOrDefault();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool SportExists(int id)
        {
            return _context.Sports.Any(s => s.Id == id);
        }

        public ICollection<Dog> GetDogsBySport(int sportId)
        {
            return _context.DogSports.Where(s => s.SportId == sportId).Select(s => s.Dog).ToList();
        }

        public bool CreateSport(Sport sport)
        {
            _context.Add(sport);
            return Save();
        }

        public bool UpdateSport(Sport sport)
        {
            _context.Update(sport);
            return Save();
        }

        public bool DeleteSport(Sport sport)
        {
            _context.Remove(sport);
            return Save();
        }
    }
}

