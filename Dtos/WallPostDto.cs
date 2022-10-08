using System;
using RealPetApi.Models;

namespace RealPetApi.Dtos
{
    public class WallPostDto
    {
        public int Id { get; set; }
        public string Body { get; set; } = string.Empty;
        public string PhotoUrl { get; set; } = string.Empty;
        public HandlerCommentDto Handler { get; set; }
        public List<CommentDto> Comments { get; set; }
    }
}

