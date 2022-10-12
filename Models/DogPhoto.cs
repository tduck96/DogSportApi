using System;
namespace RealPetApi.Models
{
    public class DogPhoto
    {
        public int Id { get; set; }
        public string Url { get; set; } = string.Empty;
        public string PublicId { get; set; } = string.Empty;
        public int DogId { get; set; }
        public Dog? Dog { get; set; }
    }
}

