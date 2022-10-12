using System;
namespace RealPetApi.Dtos
{
    public class DogPhotoDto
    {
        public int Id { get; set; }
        public string Url { get; set; } = string.Empty;
        public string PublicId { get; set; } = string.Empty;
    }
}

