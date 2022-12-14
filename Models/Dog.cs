using System;
namespace RealPetApi.Models
{
    public class Dog
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string PhotoUrl { get; set; } = string.Empty;
        public int Weight { get; set; }
        public string About { get; set; } = string.Empty;
        public string Titles { get; set; } = string.Empty;


        public ICollection<DogPhoto> ? Photos { get; set; }


        public UserProfile ? UserProfile { get; set; }
        public int UserProfileId { get; set; }

        public Breed? Breed { get; set; }
        public int BreedId { get; set; }


        public ICollection<DogSport>? DogSports { get; set; }
        public ICollection<DogTitle> DogTitles { get; set; }


    }

}

