using System;
using RealPetApi.Dtos;
using RealPetApi.Models;

namespace RealPetApi.Data
{
    public interface IHandlerRepository
    {
    
        Task<List<HandlersListDto>>GetHandlers();
        Task<Handler> GetHandler(int id);
        Task<bool> CreateHandler(Handler handlerToCreate);
        Task<bool> UpdateHandler(Handler handlerToUpdate);
        Task<bool> DeleteHandler(int handlerId);
        
    }
}

