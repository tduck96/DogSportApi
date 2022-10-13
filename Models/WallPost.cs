using System;
namespace RealPetApi.Models
{
    public class WallPost
    {
        public int Id { get; set; }
        public string Body { get; set; } = string.Empty;
        public string PhotoUrl { get; set; } = string.Empty;

        public int UserProfileId { get; set; }
        public UserProfile? UserProfile { get; set; }

        public ICollection<Comment> Comments { get; set; }

    }
}

