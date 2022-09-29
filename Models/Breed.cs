using System;
namespace RealPetApi.Models
{
    public class Breed
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public List<Dog>? Dogs { get; set; }
    }
}

