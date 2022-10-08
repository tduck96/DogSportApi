using System;
namespace RealPetApi.Dtos
{
    public class WallPostUpdateDto
    {
        public int Id { get; set; }
        public string Body { get; set; } = string.Empty;
        public string PhotoUrl { get; set; } = string.Empty;
    }
}

