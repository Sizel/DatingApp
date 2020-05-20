using DatingApp.API.Data;
using DatingApp.API.Data.Models;
using DatingApp.Data.Models;
using DatingApp.Helpers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DatingApp.API
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var host = CreateHostBuilder(args).Build();
			using (var scope = host.Services.CreateScope())
			{
				var context = scope.ServiceProvider.GetRequiredService<DataContext>();
				context.Database.Migrate();
				var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
				var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<Role>>();
				Seed.SeedUsers(userManager, roleManager);
			}

			host.Run();
		}

		public static IHostBuilder CreateHostBuilder(string[] args) =>
			Host.CreateDefaultBuilder(args)
				.ConfigureWebHostDefaults(webBuilder =>
				{
					webBuilder.UseStartup<Startup>();
				});
	}
}
