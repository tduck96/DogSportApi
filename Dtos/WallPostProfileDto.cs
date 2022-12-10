using System;
namespace RealPetApi.Dtos
{
    public class WallPostProfileDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string Body { get; set; } = string.Empty;
        public int UserId { get; set; }
        public string PhotoUrl { get; set; } = string.Empty;
        public string? UserPhotoUrl { get; set; }
        public DateTime? Date { get; set; }

    }
}

