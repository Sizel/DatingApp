using DatingApp.Data.Models;
using System;
using System.Collections.Generic;

namespace DatingApp.Data.DTOs
{
	public class DetailedUserDTO
	{
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastActive { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string MainPhotoUrl { get; set; }
        public ICollection<Photo> Photos { get; set; }
        public UserDescription UserDescription { get; set; }
    }
}
