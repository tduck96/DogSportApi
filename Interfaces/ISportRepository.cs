using System;
using RealPetApi.Models;

namespace RealPetApi.Interfaces
{
    public interface ISportRepository
    {
        ICollection<Sport> GetSports();

        Sport GetSport(int id);

        ICollection<Handler> GetHandlersBySport(int sportId);

        ICollection<Dog> GetDogsBySport(int sportId);

        ICollection<Club> GetClubsBySport(int sportId);

        bool CreateSport(Sport sport);
        bool UpdateSport(Sport sport);
        bool DeleteSport(Sport sport);
        bool SportExists(int id);
        bool Save();
    }
}

