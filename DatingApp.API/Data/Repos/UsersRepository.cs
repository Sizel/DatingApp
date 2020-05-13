using DatingApp.API.Data;
using DatingApp.API.Data.Models;
using DatingApp.Data.Models;
using DatingApp.Data.Repos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatingApp.Data
{
	public class UsersRepository : Repository<User>, IUserRepository
	{
		public UsersRepository(DataContext context) : base(context) {}
		
		public async Task<User> GetDetailedUser(int id)
		{
			var detailedUser = await Context.Users
											.Include(u => u.Photos)
											.Include(u => u.UserDescription)
											.FirstOrDefaultAsync(u => u.UserId == id);

			return detailedUser;
		}

		public async Task<List<User>> GetUsersForList()
		{
			var usersForList = await Context.Users.Include(u => u.Photos).ToListAsync<User>();

			return usersForList;
		}


	}
}
