using System;
using RealPetApi.Models;

namespace RealPetApi.Dtos
{
    public class UserListDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Bio { get; set; } = "Hello, welcome to my profile";
        public ICollection<Photo> Photos { get; set; }
        public string Location { get; set; }
    }
}

