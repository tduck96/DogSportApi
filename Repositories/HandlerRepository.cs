﻿using System;
using RealPetApi.Models;

namespace RealPetApi.Repositories
{
    public class HandlerRepository : IHandlerRepository
    {
        private readonly DataContext _context;

        public HandlerRepository(DataContext context)
        {
            _context = context;
        }

        public ICollection<Dog> GetDogsByHandler(int handlerId)
        {
            return _context.Dogs.Where(d => handlerId == handlerId).ToList();
        }

        public Handler GetHandler(int id)
        {
            return _context.Handlers.Where(h => h.Id == id).FirstOrDefault();
        }

        public ICollection<Handler> GetHandlers()
        {
            return _context.Handlers.ToList();
        }

        public bool HandlerExists(int id)
        {
            return _context.Handlers.Any(h => h.Id == id);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}

