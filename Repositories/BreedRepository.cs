using System;
using RealPetApi.Data;
using RealPetApi.Dtos;
using RealPetApi.Interfaces;
using RealPetApi.Models;

namespace RealPetApi.Repositories
{
    public class BreedRepository : IBreedRepository
    {
        private readonly DataContext _context;

        public BreedRepository(DataContext context)
        {
            _context = context;
        }

        public bool BreedExists(int id)
        {
            return _context.Breeds.Any(b => b.Id == id);
        }

      
        public ICollection<Dog> GetDogsByBreed(int breedId)
        {
            return _context.Dogs.Where(d => d.BreedId == breedId).ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }


        public async Task<IEnumerable<Breed>> GetBreeds()
        {
            return await _context.Breeds.ToListAsync();
        }

        public async Task<bool> CreateBreed(BreedDto breedCreate)
        {

            var breed = new Breed
            {
                Name = breedCreate.Name
            };

            _context.Breeds.Add(breed);

             var created = await _context.SaveChangesAsync();
             return created > 0;
        }

        public async Task<bool> UpdateBreed(Breed breedToUpdate)
        {
            _context.Breeds.Update(breedToUpdate);

            var updated = await _context.SaveChangesAsync();
            return updated > 0;
        }

        public async Task<Breed> GetBreed(int id)
        {
            return await _context.Breeds.SingleOrDefaultAsync(c => c.Id == id);
        }

        public async Task<bool> DeleteBreed(int breedId)
        {
            var breed = await GetBreed(breedId);

            _context.Breeds.Remove(breed);

            var removed = await _context.SaveChangesAsync();

            return true;

        }
    }
}

