using System;
using RealPetApi.Models;

namespace RealPetApi.Data
{
    public interface IHandlerRepository
    {
        //Get
        ICollection<Handler> GetHandlers();
        Handler GetHandler(int id);
        ICollection<Dog> GetDogsByHandler(int handlerId);

        //Post




        //Update




        //Delete

        bool HandlerExists(int id);
        bool Save();


    }
}

