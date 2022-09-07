using System;
namespace RealPetApi.Models
{
    public class Handler
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Dog> Dogs { get; set; }

        public Location Location { get; set; }
        public int LocationId { get; set; }

        public ICollection<HandlerSport> HandlerSports { get; set; }


    }
}

