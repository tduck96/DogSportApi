using System;
namespace RealPetApi.Models
{
    public class DogTitle
    {
        public int DogId { get; set; }
        public int TitleId { get; set; }

        public Dog? Dog { get; set; }
        public Title? Title { get; set; }

    }
}

