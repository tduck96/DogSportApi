using System;
using RealPetApi.Models;

namespace RealPetApi.Interfaces
{
    public interface ITitleRepository
    {
        Task<List<Title>> GetTitles();
        Task<Title> GetTitle(int titleId);
        Task<bool> UpdateTitle(Title titleToUpdate);
        Task<bool> CreateTitle(Title titleToCreate);
        Task<bool> DeleteTitle(int titleId);
    }
}

