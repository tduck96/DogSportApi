using System;
namespace RealPetApi.Dtos
{
    public class DogProfileDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string PhotoUrl { get; set; } = string.Empty;
        public int Weight { get; set; }
        public string About { get; set; } = string.Empty;
        public List<DogPhotoDto> DogPhotos { get; set; }
        public List<SportDto> Sports { get; set; }



    }
}

