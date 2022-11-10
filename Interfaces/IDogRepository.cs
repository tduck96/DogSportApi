using System;
using RealPetApi.Dtos;
using RealPetApi.Models;

namespace RealPetApi.Interfaces
{
    public interface IDogRepository
    {
        Task<ICollection<Dog>> GetDogs();

        Task<Dog>GetDog(int id);

        Task<List<SportDto>> GetSportsByDog(int dogId);

        Task<List<DogPhotoDto>> GetPhotosByDog(int dogId);

        Task<bool> CreateDog(Dog dogToCreate);

        Task<bool> UpdateDog(Dog dogToUpdate);

        Task<bool> DeleteDog(int dogId);

        Task<List<Title>> GetTitlesByDog(int dogId);

       
    }
}

