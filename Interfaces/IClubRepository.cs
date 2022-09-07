using System;
using RealPetApi.Models;

namespace RealPetApi.Interfaces
{
    public interface IClubRepository
    {
        ICollection<Club> GetClubs();

        Club GetClub(int id);

        ICollection<Sport> GetSportsByClub(int clubId);

        bool CreateClub(Club club);

        bool UpdateClub(Club club);

        bool DeleteClub(Club club);

        bool ClubExists(int id);

        bool Save();
    }
}

