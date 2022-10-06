using System;
using RealPetApi.Models;

namespace RealPetApi.Data
{
    public interface IHandlerRepository
    {
    
        Task<ICollection<Handler>>GetHandlers();
        Task<Handler> GetHandler(int id);
        Task<List<Dog>> GetDogsByHandler(int handlerId);
        Task<bool> CreateHandler(Handler handlerToCreate);
        Task<bool> UpdateHandler(Handler handlerToUpdate);
        Task<bool> DeleteHandler(int handlerId);
        
    }
}

