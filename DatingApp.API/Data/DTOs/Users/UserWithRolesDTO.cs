using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatingApp.Data.DTOs.Users
{
	public class UserWithRolesDTO
	{
		public int Id { get; set; }
		public string Username { get; set; }
		public List<string> Roles { get; set; }
	}
}
