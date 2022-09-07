using System;
using RealPetApi.Models;

namespace RealPetApi.Interfaces
{
    public interface IDogRepository
    {
        ICollection<Dog> GetDogs();
        Dog GetDog(int id);
        Dog GetDogByName(string name);
        
        bool DogExists(int dogId);
        bool DogExistsByName(string name);
        bool Save();
    }
}

