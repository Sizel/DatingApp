using DatingApp.API.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatingApp.Data.Models
{
	public class UserDescription
	{
		public int UserDescriptionId { get; set; }
		public string Description { get; set; }
		public string Interests { get; set; }
		public int UserId { get; set; }
	}
}
