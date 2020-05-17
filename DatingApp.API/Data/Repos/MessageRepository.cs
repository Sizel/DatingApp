using DatingApp.API.Data;
using DatingApp.Data.Models;
using DatingApp.Data.Pagination;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatingApp.Data.Repos
{
	public class MessageRepository : Repository<Message>, IMessagesRepository
	{
		public MessageRepository(DataContext context) : base(context) { }
		public async Task<Message> GetMessage(int messageId)
		{
			return await Context.Messages.FirstOrDefaultAsync(m => m.MessageId == messageId);
		}

		public IQueryable<Message> GetMessagesForUser()
		{
			return Context.Messages.Include(m => m.Sender)
				.ThenInclude(s => s.Photos)
				.Include(m => m.Recipient)
				.ThenInclude(r => r.Photos)
				.AsQueryable();
		}

		public IQueryable<Message> GetConversation(int userId, int recipientId)
		{
			return Context.Messages.Include(m => m.Sender)
				.ThenInclude(s => s.Photos)
				.Include(m => m.Recipient)
				.ThenInclude(r => r.Photos)
				.Where
				(m => (m.SenderId == userId && m.RecipientId == recipientId)
					||
				(m.RecipientId == userId && m.SenderId == recipientId))
				.OrderByDescending(m => m.DateSent);
		}
	}
}
