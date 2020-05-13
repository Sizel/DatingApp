using DatingApp.API.Data;
using DatingApp.API.Data.Models;
using DatingApp.Data.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace DatingApp.Helpers
{
	public static class Seed
	{
		public static void SeedUsers(DataContext context)
		{
			if (!context.Users.Any())
			{
				var userJson = System.IO.File.ReadAllText("Data/UserSeedData.json");
				var users = JsonConvert.DeserializeObject<List<User>>(userJson);

				foreach (var user in users)
				{
					byte[] passwordHash, passwordSalt;
					GenerateHashAndSalt("password", out passwordHash, out passwordSalt);
					user.Password = new Password();
					user.Password.PasswordHash = passwordHash;
					user.Password.PasswordSalt = passwordSalt;
					user.Username = user.Username.ToLower();

					context.Users.Add(user);
				}

				context.SaveChanges();
			}
		}

		private static void GenerateHashAndSalt(string password, out byte[] passwordHash, out byte[] passwordSalt)
		{
			using (var hmac = new System.Security.Cryptography.HMACSHA256())
			{
				passwordSalt = hmac.Key;
				passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
			}
		}
	}
}
