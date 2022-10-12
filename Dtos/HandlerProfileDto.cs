using System;
using RealPetApi.Models;

namespace RealPetApi.Dtos
{
    public class HandlerProfileDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Bio { get; set; } = "Hello, welcome to my profile";
        public string PhotoUrl { get; set; } = "https://www.shutterstock.com/image-photo/portrait-young-smiling-woman-looking-camera-1865153395";
        public List<PhotoDto> Photos { get; set; }

        public IEnumerable<DogDtoForUserProfile>? Dogs { get; set; }

        public LocationDto Location { get; set; }
      
    }
}

