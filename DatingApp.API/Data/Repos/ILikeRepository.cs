using DatingApp.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatingApp.Data.Repos
{
	public interface ILikeRepository
	{
		public Task<Like> GetLike(int userId, int recipientId);
		public void AddLike(Like like);
	}
}
