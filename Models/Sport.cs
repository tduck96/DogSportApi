using System;
namespace RealPetApi.Models
{
    public class Sport
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string PhotoUrl { get; set; } = string.Empty;

        public ICollection<DogSport>? DogSports { get; set; }
        public ICollection<ClubSport>? ClubSports { get; set; }
        public ICollection<HandlerSport>? HandlerSports { get; set; }


    }
}

