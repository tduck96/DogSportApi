﻿using System;
namespace RealPetApi.Dtos
{
    public class AuthHandlerDto
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public int LocationId { get; set; }


    }
}

