using System;
using RealPetApi.Models;

namespace RealPetApi.Repositories
{
    public class LocationRepository : ILocationRepository
    {
        private readonly DataContext _context;

        public LocationRepository(DataContext context)
        {
            _context = context;
        }

        public bool CreateLocation(Location location)
        {
            _context.Add(location);
            return Save();
        }

        public bool DeleteLocation(Location location)
        {
            _context.Remove(location);
            return Save();
        }

        public ICollection<Club> GetClubsByLocation(int locationId)
        {
            return _context.Clubs.Where(c => c.LocationId == locationId).ToList();
        }

        public Location GetLocation(int id)
        {
            return _context.Locations.Where(l => l.Id == id).FirstOrDefault();
        }

        public ICollection<Location> GetLocations()
        {
            return _context.Locations.ToList();
        }

        public bool LocationExists(int id)
        {
            return _context.Locations.Any(l => l.Id == id);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateLocation(Location location)
        {
            _context.Update(location);
             return Save();
        }
    }
}

