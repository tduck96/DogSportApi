using System;
using RealPetApi.Models;

namespace RealPetApi.Interfaces
{
    public interface IBreedRepository
    {
        ICollection<Breed> GetBreeds();
        Breed GetBreed(int id);
        ICollection<Dog> GetDogsByBreed(int breedId);
        bool CreateBreed(Breed breed);
        bool UpdateBreed(Breed breed);
        bool DeleteBreed(Breed breed);

        bool BreedExists(int id);
        bool Save();
    }
}

