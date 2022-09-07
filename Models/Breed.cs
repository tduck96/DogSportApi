using System;
namespace RealPetApi.Models
{
    public class Breed
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Dog> Dogs { get; set; }
    }
}

