using System;
namespace RealPetApi.Dtos
{
	public class ClubCreateDto
	{
        public int Id { get; set; }
        public string Name { get; set; }
        public string About { get; set; } = "We are a dog training facility";
        public int Founded { get; set; } = 2015;
        public int LocationId { get; set; }
        public string PhotoUrl { get; set; }
    }
}

