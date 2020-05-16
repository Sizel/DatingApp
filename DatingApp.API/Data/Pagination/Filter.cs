using DatingApp.API.Data.Models;
using DatingApp.Data.Models;
using Microsoft.AspNetCore.Mvc.Diagnostics;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DatingApp.Data.Pagination
{
	public static class Filter
	{
		public static IQueryable<User> FilterUsers(IQueryable<User> users, PaginationParams paginationParams, User userWhoMakeRequest)
		{
			paginationParams.UserId = userWhoMakeRequest.UserId;
			// Не показывать пользователю самого себя
			users = users.Where(u => u.UserId != paginationParams.UserId);

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
	}
}
