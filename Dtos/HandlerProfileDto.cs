using System;
using RealPetApi.Models;

namespace RealPetApi.Dtos
{
    public class HandlerProfileDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;

        public IEnumerable<DogDto>? Dogs { get; set; }

        public LocationDto? Location { get; set; }
      
    }
}

