using System;
using Microsoft.AspNetCore.Mvc;
using RealPetApi.Models;

namespace RealPetApi.Dtos
{
    public class DogDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Weight { get; set; }
        public string About { get; set; }
        public string PhotoUrl { get; set; }
        public int BreedId { get; set; }
        public int LocationId { get; set; }
        public int UserProfileId { get; set; }

    }
}

