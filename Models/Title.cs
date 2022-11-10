using System;
namespace RealPetApi.Models
{
    public class Title
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int DogId { get; set; }
        public ICollection<DogTitle>? DogTitles { get; set; }

    }
}

