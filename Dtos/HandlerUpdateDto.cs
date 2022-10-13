using System;
namespace RealPetApi.Dtos
{
    public class HandlerUpdateDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Bio { get; set; } = string.Empty;
        public int LocationId { get; set; }
    }
}

