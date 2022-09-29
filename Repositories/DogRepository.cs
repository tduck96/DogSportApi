﻿using System;
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


    

      

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

      

        public async Task<bool> CreateDog(Dog dogToCreate)
        {
            await _context.Dogs.AddAsync(dogToCreate);
            var created = await _context.SaveChangesAsync();
            return created > 0;
        }

        public async Task<Dog> GetDog(int id)
        {
            return await _context.Dogs.Where(d => d.Id == id).FirstOrDefaultAsync();
        }

        public async Task<ICollection<Dog>> GetDogs()
        {
            return await _context.Dogs.ToListAsync();
        }

        public async Task<bool> UpdateDog(Dog dogToUpdate)
        {
            _context.Dogs.Update(dogToUpdate);
            var updated = await _context.SaveChangesAsync();
            return updated > 0;

        }

        public async Task<bool> DeleteDog(int dogId)
        {
            var dog = await GetDog(dogId);

            _context.Dogs.Remove(dog);

            var deleted = await _context.SaveChangesAsync();

            return true;
        }

        public Task<bool> DogExists(int dogId)
        {
            throw new NotImplementedException();
        }
    }
}

