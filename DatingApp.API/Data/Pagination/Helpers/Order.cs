using DatingApp.API.Data.Models;
using DatingApp.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatingApp.Data.Pagination
{
	public static class Order
	{
		public static IOrderedQueryable<User> OrderUsers(IQueryable<User> users, UserPaginationParams userPaginationParams)
		{
			IOrderedQueryable<User> orderedUsers;
			switch (userPaginationParams.OrderBy)
			{
				case "ageAsc":
					orderedUsers = users.OrderByDescending(u => u.DateOfBirth);
					break;
				case "ageDesc":
					orderedUsers = users.OrderBy(u => u.DateOfBirth);
					break;
				default:
					orderedUsers = users.OrderByDescending(u => u.LastActive);
					break;
			}
			return orderedUsers;
		}

		public static IOrderedQueryable<Message> OrderMessages(IQueryable<Message> messages, MessagePaginationParams messageParams)
		{
			return messages.OrderByDescending(m => m.DateSent);
		}
	}
}
