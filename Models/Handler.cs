using System;
namespace RealPetApi.Models
{
    public class Handler
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public byte[] PasswordHash { get; set; } = new byte[32];
        public byte[] PasswordSalt { get; set; } = new byte[32];
        public string RefreshToken { get; set; } = string.Empty;
        public DateTime TokenCreated { get; set; }
        public DateTime TokenExpires { get; set; }
        public string Role { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;

        public List<Dog> Dogs { get; set; }
        public Location Location { get; set; }
        public int LocationId { get; set; }  
        public ICollection<HandlerSport> HandlerSports { get; set; }


    }
}

