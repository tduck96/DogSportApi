using System;
using RealPetApi.Models;

namespace RealPetApi.Interfaces
{
    public interface IDogRepository
    {
        Task<ICollection<Dog>> GetDogs();

        Task<Dog>GetDog(int id);

        Task<bool> CreateDog(Dog dogToCreate);

        Task<bool> UpdateDog(Dog dogToUpdate);

        Task<bool> DeleteDog(int dogId);

        Task<bool> DogExists(int dogId);

        bool Save();
    }
}

