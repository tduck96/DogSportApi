using System;
namespace RealPetApi.Models
{
    public class HandlerSport
    {
        public int UserProfileId { get; set; }
        public int SportId { get; set; }

        public UserProfile UserProfile { get; set; }
        public Sport Sport { get; set; }



    }
}

