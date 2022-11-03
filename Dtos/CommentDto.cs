using System;
namespace RealPetApi.Dtos
{
    public class CommentDto
    {
        public int Id { get; set; }
        public string Body { get; set; } = string.Empty;
        public string WriterName { get; set; } = String.Empty; 
        public string WriterPhotoUrl { get; set; } = string.Empty;


    }
}

