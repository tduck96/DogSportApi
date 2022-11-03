using System;
namespace RealPetApi.Dtos
{
    public class CommentCreateDto
    {
        public int Id { get; set; }
        public string Body { get; set; } = string.Empty;

    }
}

