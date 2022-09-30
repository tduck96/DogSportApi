using System;
using RealPetApi.Models;

namespace RealPetApi.Dtos
{
    public class DogDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int LocationId { get; set; }
        public int HandlerId { get; set; }
        public int BreedId { get; set; }
        public Breed Breed { get; set; }
        public Handler Handler { get; set; }


    }
}

