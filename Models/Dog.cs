﻿using System;
namespace RealPetApi.Models
{
    public class Dog
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string PhotoUrl { get; set; } = string.Empty;
        public int Weight { get; set; }
        public string About { get; set; } = string.Empty;



        public Handler ? Handler { get; set; }
        public int HandlerId { get; set; }

        public Breed? Breed { get; set; }
        public int BreedId { get; set; }

        public Location? Location { get; set; }
        public int? LocationId { get; set; }

        public ICollection<DogSport>? DogSports { get; set; }


    }

}

