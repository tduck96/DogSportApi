using System;
using RealPetApi.Models;

namespace RealPetApi.Dtos
{
    public class HandlerProfile
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;

        public List<Dog>? Dogs { get; set; }
        public Location? Location { get; set; }
        public ICollection<HandlerSport>? HandlerSports { get; set; }
    }
}

