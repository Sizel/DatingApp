using DatingApp.API.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatingApp.Data.Models
{
	public class Password
	{
		public int PasswordId { get; set; }
		public byte[] PasswordHash { get; set; }
		public byte[] PasswordSalt { get; set; }
		virtual public User User { get; set; }
		public int UserId { get; set; }
	}
}
