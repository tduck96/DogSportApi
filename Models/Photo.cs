using System;
namespace RealPetApi.Models
{
    public class Photo
    {
        public int Id { get; set; }
        public string Url { get; set; } = string.Empty;
        public string PublicId { get; set; } = string.Empty;
        public bool ProfilePic { get; set; }
        public int UserProfileId { get; set; }
        public UserProfile? UserProfile { get; set; }

    }
}

