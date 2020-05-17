using DatingApp.Data.Models;
using DatingApp.Data.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatingApp.Data.Repos
{
	public interface IMessagesRepository : IRepository<Message>
	{
		public Task<Message> GetMessage(int messageId);
		public IQueryable<Message> GetConversation(int userId, int recipientId);
		public IQueryable<Message> GetMessagesForUser();
	}
}
