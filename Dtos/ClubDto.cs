using System;
using RealPetApi.Models;

namespace RealPetApi.Dtos
{
    public class ClubDto
    {
       
        public int Id { get; set; }
        public string Name { get; set; }
        public string About { get; set; } = "We are a dog training facility";
        public int Founded { get; set; } = 2015;
        public string location { get; set; }
        public int LocationId { get; set; }
        public List<SportDto> Sports { get; set; }





    }
    
    
}

