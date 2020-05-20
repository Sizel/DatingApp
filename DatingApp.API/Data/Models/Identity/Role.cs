using DatingApp.Data.Models.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatingApp.Data.Models
{
	public class Role : IdentityRole<int>
	{
		public ICollection<UserRole> UserRoles { get; set; }
	}
}
