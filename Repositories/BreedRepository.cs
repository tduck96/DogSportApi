using System;
using RealPetApi.Data;
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

        public bool CreateBreed(Breed breed)
        {
            _context.Add(breed);
            return Save();
        }

        public bool DeleteBreed(Breed breed)
        {
            _context.Remove(breed);
            return Save();
        }

        public Breed GetBreed(int id)
        {
            return _context.Breeds.Where(b => b.Id == id).FirstOrDefault();
        }

        public ICollection<Breed> GetBreeds()
        {
            return _context.Breeds.ToList();
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

        public bool UpdateBreed(Breed breed)
        {
            _context.Update(breed);
            return Save();
        }
    }
}

