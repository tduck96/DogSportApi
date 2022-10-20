using System;
namespace RealPetApi.Models
{
    public class Handler
    {
        public int Id { get; set; }
        public string Role { get; set; } = "handler";
        public string Email { get; set; } = string.Empty;
        public byte[] PasswordHash { get; set; } = new byte[32];
        public byte[] PasswordSalt { get; set; } = new byte[32];
        public string RefreshToken { get; set; } = string.Empty;
        public DateTime TokenCreated { get; set; }
        public DateTime TokenExpires { get; set; }

       
        public UserProfile UserProfile { get; set; }
        //public int UserHandlerId { get; set; }





    }
}

