using DatingApp.Data.Models;
using DatingApp.Data.Models.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace DatingApp.API.Data.Models
{
    public class User : IdentityUser<int>
    {
        public string Gender { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastActive { get; set; }
        public UserDescription UserDescription { get; set; }
        public ICollection<Photo> Photos { get; set; }
        public ICollection<Like> Likees { get; set; }
        public ICollection<Like> Likers { get; set; }
        public ICollection <Message> MessagesSent { get; set; }
        public ICollection<Message> MessagesRecieved { get; set; }
        public ICollection<UserRole> UserRoles { get; set; }
    }
}