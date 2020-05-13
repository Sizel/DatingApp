using DatingApp.API.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatingApp.Data.Repos
{
	public interface IUserRepository : IRepository<User>
	{
		public Task<User> GetDetailedUser(int id);
		public Task<List<User>> GetUsersForList();
	}
}
