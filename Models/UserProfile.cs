using System;
namespace RealPetApi.Models
{
    public class UserProfile
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Bio { get; set; } = string.Empty;
        public string PhotoUrl { get; set; } = "https://res.cloudinary.com/dx58mbwcg/image/upload/v1664587012/cld-sample.jpg";
        public int LocationId { get; set; }
        public int HandlerId { get; set; }
        public Handler? Handler { get; set; }
        public List<Photo>? Photos { get; set; }
        public ICollection<WallPost>? Wallposts { get; set; }
        public ICollection<Comment>? Comments { get; set; }
        public List<Dog>? Dogs { get; set; }
        public Location? Location { get; set; }
        public ICollection<HandlerSport>? HandlerSports { get; set; }

        public ICollection<UserFollowing>?UserFollowing { get; set; }
        public ICollection<UserFollowing> ?Followers { get; set; }


    }
}

