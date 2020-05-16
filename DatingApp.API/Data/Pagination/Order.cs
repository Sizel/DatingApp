using DatingApp.API.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatingApp.Data.Pagination
{
	public static class Order
	{
		public static IOrderedQueryable<User> OrderUsers(IQueryable<User> users, PaginationParams paginationParams)
		{
			IOrderedQueryable<User> orderedUsers;
			switch (paginationParams.OrderBy)
			{
				case "lastActive":
					orderedUsers = users.OrderByDescending(u => u.LastActive);
					break;
				case "created":
					orderedUsers = users.OrderByDescending(u => u.Created);
					break;
				default:
					orderedUsers = users.OrderByDescending(u => u.LastActive);
					break;
			}
			return orderedUsers;
		}
	}
}
