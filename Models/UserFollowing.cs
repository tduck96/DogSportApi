using System;
namespace RealPetApi.Models
{
    public class UserFollowing
    {
        public int? UserProfileId { get; set; }
        public int? UserFollowsId { get; set; }

        public UserProfile? UserProfile { get; set; }
        public UserProfile? UserFollows { get; set; }
    }
}

