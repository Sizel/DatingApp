using DatingApp.Data.Models;
using System;
using System.Collections.Generic;

namespace DatingApp.API.Data.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Gender { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastActive { get; set; }
        public Password Password { get; set; }
        public UserDescription UserDescription { get; set; }
        public ICollection<Photo> Photos { get; set; }
        public ICollection<Like> Likees { get; set; }
        public ICollection<Like> Likers { get; set; }
        public ICollection <Message> MessagesSent { get; set; }
        public ICollection<Message> MessagesRecieved { get; set; }
    }
}