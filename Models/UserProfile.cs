using System;
namespace RealPetApi.Models
{
    public class UserProfile
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Bio { get; set; } = string.Empty;
        public string PhotoUrl { get; set; } = string.Empty;
        public int LocationId { get; set; }
        public int HandlerId { get; set; }
        public Handler Handler { get; set; }
        public List<Photo> Photos { get; set; }
        public ICollection<WallPost> Wallposts { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public List<Dog>? Dogs { get; set; }
        public Location? Location { get; set; }
        public ICollection<HandlerSport> HandlerSports { get; set; }

    }
}

