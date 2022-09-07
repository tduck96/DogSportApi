using System;
using RealPetApi.Models;

namespace RealPetApi.Repositories
{
    public class DogRepository : IDogRepository
    {
        private readonly DataContext _context;

        public DogRepository(DataContext context)
        {
            _context = context;
        }

        public bool DogExists(int dogId)
        {
            return _context.Dogs.Any(d => d.Id == dogId);
        }

        public bool DogExistsByName(string dogName)
        {
            return _context.Dogs.Any(d => d.Name == dogName);
        }

        public Dog GetDog(int id)
        {
            return _context.Dogs.Where(d => d.Id == id).FirstOrDefault();
        }

        public Dog GetDogByName(string name)
        {
            return _context.Dogs.Where(d => d.Name == name).FirstOrDefault();
        }

        public ICollection<Dog> GetDogs()
        {
            return _context.Dogs.ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}

