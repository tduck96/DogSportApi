using System;
namespace RealPetApi.Models
{
    public class Photo
    {
        public int Id { get; set; }
        public string Url { get; set; } = string.Empty;
        public string PublicId { get; set; } = string.Empty;
        public int UserProfileId { get; set; }
        public UserProfile? Profile { get; set; }

    }
}

