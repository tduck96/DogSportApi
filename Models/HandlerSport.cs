using System;
namespace RealPetApi.Models
{
    public class HandlerSport
    {
        public int HandlerId { get; set; }
        public int SportId { get; set; }

        public Handler Handler { get; set; }
        public Sport Sport { get; set; }



    }
}

