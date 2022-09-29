using System;
namespace RealPetApi.Models
{
    public class Location
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public List<Dog>? Dogs { get; set; }
        public List<Handler>? Handlers { get; set; }

        public List<Club>? Clubs { get; set; }

    }
}

