using System;
using RealPetApi.Models;

namespace RealPetApi.Interfaces
{
    public interface IBreedRepository
    {
        ICollection<Breed> GetBreeds();
        Breed GetBreed(int id);
        ICollection<Dog> GetDogsByBreed(int breedId);


        bool BreedExists(int id);
        bool Save();
    }
}

