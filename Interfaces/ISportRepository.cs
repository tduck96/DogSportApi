using System;
using RealPetApi.Dtos;
using RealPetApi.Models;

namespace RealPetApi.Interfaces
{
    public interface ISportRepository
    {
        Task<ICollection<Sport>> GetSports();

        Task<Sport> GetSport(int id);

        Task<ICollection<UserProfile>> GetUsersBySport(int sportId);

        

        Task<ICollection<Dog>> GetDogsBySport(int sportId);

        Task<List<ClubDto>> GetClubsBySport(int sportId);

        Task<bool> CreateSport(Sport sportToCreate);
        Task<bool> UpdateSport(Sport sportToUpdate);
        Task<bool> DeleteSport(int sportId);
        
    }
}

