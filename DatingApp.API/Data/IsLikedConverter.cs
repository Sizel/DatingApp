using AutoMapper;
using DatingApp.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatingApp.Data
{
	public class IsLikedConverter : IValueConverter<ICollection<Like>, bool>
	{
		public bool Convert(ICollection<Like> likers, ResolutionContext context)
		{
			if (likers != null)
			{
				return likers.FirstOrDefault(l => l.LikerId == (int)context.Items["idFromToken"]) != null;
			}
			else
			{
				return false;
			}
		}
	}
}
