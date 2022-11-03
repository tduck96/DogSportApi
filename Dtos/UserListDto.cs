using System;
using RealPetApi.Models;

namespace RealPetApi.Dtos
{
    public class UserListDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Bio { get; set; } = "Hello, welcome to my profile";
        public string photoUrl { get; set; } = string.Empty;
        public List<string> Urls { get; set; }
        public string Location { get; set; }
    }
}

