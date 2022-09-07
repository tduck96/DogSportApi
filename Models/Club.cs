using System;
namespace RealPetApi.Models
{
    public class Club
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Location Location { get; set; }
        public int LocationId { get; set; }

        public ICollection<ClubSport> ClubSports { get; set; }

    }
}

