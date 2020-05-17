using DatingApp.API.Data.Models;
using DatingApp.Data.Models;
using DatingApp.Data.Pagination;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatingApp.Data.Repos
{
	public interface IUserRepository : IRepository<User>
	{
		public Task<User> GetDetailedUser(int id);
		public IQueryable<User> GetUsers();
		public Task<User> GetUserWithDescr(int id);
		public Task<IEnumerable<int>> GetLikersIds(int userId);
		public Task<IEnumerable<int>> GetLikeesIds(int userId);
	}
}
