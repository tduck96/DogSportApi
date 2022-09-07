using System;
using RealPetApi.Models;

namespace RealPetApi.Interfaces
{
    public interface IClubRepository
    {
        ICollection<Club> GetClubs();
        Club GetClub(int id);
        ICollection<Sport> GetSportsByClub(int clubId);


        bool ClubExists(int id);
        bool Save();
    }
}

