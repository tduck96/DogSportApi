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

        public bool CreateDog( Dog dog)
        {
            _context.Add(dog);
            return Save();
        }

        public bool DeleteDog(Dog dog)
        {
            _context.Remove(dog);
            return Save();
        }

        public bool DogExists(int dogId)
        {
            return _context.Dogs.Any(d => d.Id == dogId);
        }


        public Dog GetDog(int id)
        {
            return _context.Dogs.Where(d => d.Id == id).FirstOrDefault();
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

        
        public bool UpdateDog(Dog dog)
        {
            _context.Update(dog);
            return Save();
        }
    }
}

