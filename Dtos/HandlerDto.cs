using System;
using RealPetApi.Models;

namespace RealPetApi.Dtos
{
    public class HandlerDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int LocationId { get; set; }
    }
}

