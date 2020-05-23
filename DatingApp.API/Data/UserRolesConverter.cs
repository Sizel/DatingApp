using AutoMapper;
using DatingApp.Data.Models.Identity;
using System.Collections.Generic;
using System.Linq;

namespace DatingApp.Data.Models
{
	public class UserRolesConverter : IValueConverter<ICollection<UserRole>, List<string>>
	{
		public List<string> Convert(ICollection<UserRole> userRoles, ResolutionContext context)
		{
			return userRoles.Select(ur => ur.Role.Name).ToList();
		}
	}
}
