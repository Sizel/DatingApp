using DatingApp.API.Data;
using DatingApp.API.Data.Models;
using DatingApp.Data.Models;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace DatingApp.Helpers
{
	public static class Seed
	{
		public async static void SeedUsers(UserManager<User> userManager, RoleManager<Role> roleManager)
		{
			if (!userManager.Users.Any())
			{
				var userJson = System.IO.File.ReadAllText("Data/UserSeedData.json");
				var users = JsonConvert.DeserializeObject<List<User>>(userJson);

				var roles = new List<Role>
				{
					new Role { Name = "Member" },
					new Role { Name = "Moderator" },
					new Role { Name = "Admin" }
				};

				foreach (var role in roles)
				{
					await roleManager.CreateAsync(role);
				};

				foreach (var user in users)
				{
					await userManager.CreateAsync(user, "password");
					if (user.UserName == "Admin")
					{
						await userManager.AddToRolesAsync(user, new[] { "Admin", "Member", "Moderator" });
					}
					else
					{
						await userManager.AddToRoleAsync(user, "Member");
					}
				}
			}
		}
	}
}
