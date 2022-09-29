using System;
using RealPetApi.Dtos;
using RealPetApi.Models;

namespace RealPetApi.Interfaces
{
    public interface IBreedRepository
    {
        Task<IEnumerable<Breed>> GetBreeds();
        Task<Breed> GetBreed(int id);
        ICollection<Dog> GetDogsByBreed(int breedId);
        Task<bool> CreateBreed(BreedDto breedCreate);
        Task<bool> UpdateBreed(Breed breedToUpdate);
        Task<bool> DeleteBreed(int breedId);

        bool BreedExists(int id);
        bool Save();
    }
}

