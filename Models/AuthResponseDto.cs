using System;
namespace RealPetApi.Models
{
    public class AuthResponseDto
    {
        public bool Success { get; set; } = false;
        public string Message { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime TokenExpires { get; set; }
        public string Role { get; set; } = "handler";
    }
}

