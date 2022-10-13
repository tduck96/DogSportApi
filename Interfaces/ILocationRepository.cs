using System;
using RealPetApi.Models;

namespace RealPetApi.Interfaces
{
    public interface ILocationRepository
    {
        Task <ICollection<Location>> GetLocations();

        Task <Location> GetLocation(int id);

        Task <List<Club>> GetClubsByLocation(int locationId);

        Task<List<UserProfile>> GetUsersByLocation(int locationId);

        Task<bool> CreateLocation(Location location);

        Task<bool> UpdateLocation(Location location);

        Task<bool> DeleteLocation(int locationId);

    }
}

