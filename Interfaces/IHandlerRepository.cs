using System;
using RealPetApi.Models;

namespace RealPetApi.Data
{
    public interface IHandlerRepository
    {
    
        ICollection<Handler> GetHandlers();
        Handler GetHandler(int id);
        ICollection<Dog> GetDogsByHandler(int handlerId);
        bool CreateHandler(Handler handler);
        bool UpdateHandler(Handler handler);
        bool DeleteHandler(Handler handler);
        bool HandlerExists(int id);
        bool Save();


    }
}

