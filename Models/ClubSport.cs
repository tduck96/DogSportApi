using System;
namespace RealPetApi.Models
{
    public class ClubSport
    {
        public int ClubId { get; set; }
        public int SportId { get; set; }

        public Club Club { get; set; }
        public Sport Sport { get; set; }

    }
}

