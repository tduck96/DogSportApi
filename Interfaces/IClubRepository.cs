using System;
using RealPetApi.Dtos;
using RealPetApi.Models;

namespace RealPetApi.Interfaces
{
    public interface IClubRepository
    {
        Task<IEnumerable<Club>> GetClubs();

        Task<Club> GetClub(int id);

        ICollection<Sport> GetSportsByClub(int clubId);

        Task<bool> CreateClub(Club clubToCreate);

        Task<bool> UpdateClub(Club clubToUpdate);

        Task<bool> DeleteClub(int clubId);

        bool ClubExists(int id);

       Task<bool> Save();
    }
}

