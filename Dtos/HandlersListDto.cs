using System;
using RealPetApi.Models;

namespace RealPetApi.Dtos
{
    public class HandlersListDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string PhotoUrl { get; set; } = string.Empty;
        public string Location { get; set; }
        

    }
}

