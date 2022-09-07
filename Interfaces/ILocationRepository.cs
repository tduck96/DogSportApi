using System;
using RealPetApi.Models;

namespace RealPetApi.Interfaces
{
    public interface ILocationRepository
    {
        ICollection<Location> GetLocations();
        Location GetLocation(int id);
        ICollection<Club> GetClubsByLocation(int locationId);


        bool LocationExists(int id);
        bool Save();
    }
}

