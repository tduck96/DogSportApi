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
        public List<WallPostProfileDto> WallPosts { get; set; }
        public List<PhotoDto> Photos { get; set; }

        public List<DogDtoForUserProfile> Dogs { get; set; }

        public LocationDto Location { get; set; }
      
    }
}

