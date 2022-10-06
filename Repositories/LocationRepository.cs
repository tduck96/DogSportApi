using System;
using System.Security.Cryptography;
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

        public async Task<bool> CreateLocation(Location locationToCreate)
        {
            await _context.Locations.AddAsync(locationToCreate);
            var created = await _context.SaveChangesAsync();
            return created > 0;
        }

        public async Task<bool> DeleteLocation(int locationId)
        {
            var location = await GetLocation(locationId);
            _context.Locations.Remove(location);
            var deleted = await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Club>> GetClubsByLocation(int locationId)
        {
            return await _context.Clubs.Where(c => c.LocationId == locationId).ToListAsync();
            
        }

        public async Task<List<Handler>> GetHandlersByLocation(int locationId)
        {
            return await _context.Handlers.Where(c => c.LocationId == locationId).ToListAsync();
        }

        public async Task<Location> GetLocation(int id)
        {
            return await _context.Locations.FirstOrDefaultAsync(l => l.Id == id);
            
        }

        public async Task<ICollection<Location>> GetLocations()
        {
            var LocationList = _context.Locations.ToListAsync();

            return await LocationList;
        }

        public async Task<bool> UpdateLocation(Location locationToUpdate)
        {
            _context.Locations.Update(locationToUpdate);
            var updated = await _context.SaveChangesAsync();
            return updated > 0;
        }
    }
}

