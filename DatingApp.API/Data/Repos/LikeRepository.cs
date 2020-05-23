using DatingApp.API.Data;
using DatingApp.Data.Models;
using System.Threading.Tasks;

namespace DatingApp.Data.Repos
{
	public class LikeRepository : ILikeRepository
	{
		private readonly DataContext context;

		public LikeRepository(DataContext context)
		{
			this.context = context;
		}
		public async Task<Like> GetLike(int userId, int recipientId)
		{
			return await context.Likes.FindAsync(userId, recipientId);
		}

		public async void AddLike(Like like)
		{
			context.Likes.Add(like);
			await context.SaveChangesAsync();
		}

		public async void DeleteLike(Like like)
		{
			context.Likes.Remove(like);
			await context.SaveChangesAsync();
		}
	}
}
