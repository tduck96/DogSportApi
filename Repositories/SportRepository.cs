using System;
using RealPetApi.Dtos;
using RealPetApi.Models;

namespace RealPetApi.Repositories
{
    public class SportRepository : ISportRepository
    {
        private readonly DataContext _context;
        private readonly ILocationRepository _locationRepository;

        public SportRepository(DataContext context,
            ILocationRepository locationRepository
            )
        {
            _context = context;
            _locationRepository = locationRepository;
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

        public  async Task<List<ClubDto>> GetClubsBySport(int sportId)
        {
           var clubs =  await _context.ClubSports
                .Where(c => c.SportId == sportId)
                .Select(c => c.Club)
                .ToListAsync();

            List<ClubDto> Dtos = new List<ClubDto>();

            foreach (Club club in clubs)
            {
                var clubLocation = await _locationRepository.GetLocation(club.Id);

                var dto = new ClubDto
                {
                    Id = club.Id,
                    Founded = club.Founded,
                    Name = club.Name,
                    About = club.About,
                    Location = clubLocation.Name,
                    PhotoUrl = "https://res.cloudinary.com/dx58mbwcg/image/upload/v1668193137/Screen_Shot_2022-11-11_at_12.58.24_PM_nsq3za.png"

                };
                Dtos.Add(dto);
            }
            return Dtos;

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
            return await _context.HandlerSports
                .Where(e => e.SportId == sportId)
                .Select(c => c.UserProfile)
                .ToListAsync();
        }
    }
}

