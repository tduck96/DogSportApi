using System;
using RealPetApi.Models;

namespace RealPetApi.Interfaces
{
    public interface ILocationRepository
    {
        ICollection<Location> GetLocations();

        Location GetLocation(int id);

        ICollection<Club> GetClubsByLocation(int locationId);

        bool CreateLocation(Location location);

        bool UpdateLocation(Location location);

        bool DeleteLocation(Location location);

        bool LocationExists(int id);

        bool Save();
    }
}

