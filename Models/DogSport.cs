using System;
namespace RealPetApi.Models
{
    public class DogSport
    {
        public int DogId { get; set; }
        public int SportId { get; set; }

        public Dog? Dog { get; set; }
        public Sport? Sport { get; set; }

    }
}

