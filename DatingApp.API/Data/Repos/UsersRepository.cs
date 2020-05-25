using DatingApp.API.Data;
using DatingApp.API.Data.Models;
using DatingApp.Data.Repos;
using Microsoft.EntityFrameworkCore;
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
				.Include(u => u.Likers)
				.Include(u => u.UserDescription)
				.FirstOrDefaultAsync(u => u.Id == id);

			return detailedUser;
		}
		public async Task<User> GetUserWithPhotos(int id)
		{
			var detailedUser = await Context.Users
				.Include(u => u.Photos)
				.FirstOrDefaultAsync(u => u.Id == id);

			return detailedUser;
		}

		public IQueryable<User> GetUsers()
		{
			var users = Context.Users
				.Include(u => u.Photos).Include(u => u.Likers);

			return users;
		}

		public async Task<IEnumerable<int>> GetLikersIds(int userId)
		{
			var userWithLikers = await Context.Users.Include(u => u.Likers)
													.FirstOrDefaultAsync(u => u.Id == userId);

			return userWithLikers.Likers.Select(l => l.LikerId);
		}

		public async Task<IEnumerable<int>> GetLikeesIds(int userId)
		{
			var userWithLikees = await Context.Users.Include(u => u.Likees)
													.FirstOrDefaultAsync(u => u.Id == userId);

			return userWithLikees.Likees.Select(l => l.LikeeId);
		}

		public async Task<User> GetUserWithDescr(int id)
		{
			var userWithDescr = await Context.Users
											.Include(u => u.UserDescription)
											.FirstOrDefaultAsync(u => u.Id == id);

			return userWithDescr;
		}
	}
}
