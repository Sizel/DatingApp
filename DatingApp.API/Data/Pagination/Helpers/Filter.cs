using DatingApp.API.Data.Models;
using DatingApp.Data.Models;
using DatingApp.Data.Repos;
using Microsoft.AspNetCore.Mvc.Diagnostics;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatingApp.Data.Pagination
{
	public static class Filter
	{
		public async static Task<IQueryable<User>> FilterUsers(IQueryable<User> users, UserPaginationParams paginationParams, int requestingUserId, IUserRepository userRepo)
		{
			var userWhoMakeRequest = await userRepo.Get(requestingUserId);
			// Не показывать пользователю самого себя
			users = users.Where(u => u.Id != requestingUserId);

			if (paginationParams.Likees)
			{
				var likeesIds = await userRepo.GetLikeesIds(requestingUserId);
				users = users.Where(u => likeesIds.Contains(u.Id));
			}
			if (paginationParams.Likers)
			{
				var likersIds = await userRepo.GetLikersIds(requestingUserId);
				users = users.Where(u => likersIds.Contains(u.Id));
			}

			// Если пользователь не указал пол, то будет показываться противоположный
			if (paginationParams.Gender == null)
			{
				paginationParams.Gender = (userWhoMakeRequest.Gender == "male") ? "female" : "male";
			}
			else if (paginationParams.Gender != "both")
			{
				users = users.Where(u => u.Gender == paginationParams.Gender);
			}

			// Если пользователь указал минимальный или максимальный возраст, то перевести его в дату
			if (paginationParams.MinAge != 0 || paginationParams.MaxAge != 99)
			{
				var minDob = DateTime.Now.AddYears(-paginationParams.MaxAge - 1);
				var maxDob = DateTime.Now.AddYears(-paginationParams.MinAge);
				users = users.Where(u => u.DateOfBirth >= minDob && u.DateOfBirth <= maxDob);
			}

			return users;
			
		}

		public static IQueryable<Message> FilterMessages(IQueryable<Message> messages, int requestingUserId, MessagePaginationParams messageParams)
		{
			switch (messageParams.MessageType)
			{
				case "inbox":
					messages = messages.Where(m => m.RecipientId == requestingUserId);
					break;
				case "outbox":
					messages = messages.Where(m => m.SenderId == requestingUserId);
					break;
				default:
					messages = messages.Where(m => m.RecipientId == requestingUserId && m.IsRead == false);
					break;
			}

			return messages;
		}
	}
}
