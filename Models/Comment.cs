using System;
namespace RealPetApi.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Body { get; set; } = string.Empty;

        public int? UserProfileId { get; set; }
        public UserProfile? UserProfile { get; set; }

        public int? WallPostId { get; set; }
        public WallPost? WallPost { get; set; }


    }
}

