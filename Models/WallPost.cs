using System;
namespace RealPetApi.Models
{
    public class WallPost
    {
        public int Id { get; set; }
        public string Body { get; set; } = string.Empty;
        public string PhotoUrl { get; set; } = string.Empty;

        public int HandlerId { get; set; }
        public Handler? Handler { get; set; }

        public ICollection<Comment> Comments { get; set; }

    }
}

