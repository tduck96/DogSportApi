using System;
using System.Security.Cryptography;
using RealPetApi.Dtos;
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

        public async Task<List<ClubDto>> GetClubsByLocation(int locationId)
        {
           var clubs = await _context.Clubs
                .Where(c => c.LocationId == locationId)
                .ToListAsync();

            List<ClubDto> Dtos = new List<ClubDto>();

            foreach (Club club in clubs)
            {
                var location = await GetLocation(locationId);

                var dto = new ClubDto
                {
                    Id = club.Id,
                    Founded = club.Founded,
                    Name = club.Name,
                    About = club.About,
                    Location = location.Name,
                    PhotoUrl = "https://res.cloudinary.com/dx58mbwcg/image/upload/v1668193137/Screen_Shot_2022-11-11_at_12.58.24_PM_nsq3za.png"
                };

                Dtos.Add(dto);
            }
            return Dtos;
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

        public async Task<List<UserListDto>> GetUsersByLocation(int locationId)
        {
            var users =  await _context.UserProfiles.Where(c => c.LocationId == locationId)
                .Include(c => c.Location).
                ToListAsync();

            List<UserListDto> Dtos = new List<UserListDto>();

            foreach (UserProfile user in users)
            {

                var dto = new UserListDto
                {
                    Id = user.Id,
                    Bio = user.Bio,
                    Name = user.Name,
                    Location = user.Location.Name,
                    photoUrl = user.PhotoUrl

                };

                Dtos.Add(dto);
            }
            return Dtos;

        }

        public async Task<bool> UpdateLocation(Location locationToUpdate)
        {
            _context.Locations.Update(locationToUpdate);
            var updated = await _context.SaveChangesAsync();
            return updated > 0;
        }
    }
}

