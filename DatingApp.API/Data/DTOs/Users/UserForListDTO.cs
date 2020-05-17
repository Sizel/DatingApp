﻿using System;

namespace DatingApp.Data.DTOs
{
	public class UserForListDTO
	{
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastActive { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string MainPhotoUrl { get; set; }
    }
}