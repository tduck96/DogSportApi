using System;
using RealPetApi.Models;

namespace RealPetApi.Interfaces
{
    public interface IDogRepository
    {
        ICollection<Dog> GetDogs();

        Dog GetDog(int id);

        bool CreateDog( Dog dog);

        bool UpdateDog( Dog dog);

        bool DeleteDog(Dog dog);

        bool DogExists(int dogId);

        bool Save();
    }
}

