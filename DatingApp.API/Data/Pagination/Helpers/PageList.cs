using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatingApp.Data.Pagination
{
	public class PageList<T> : List<T>
	{
		public int TotalItems { get; set; }
		public int PageSize { get; set; }
		public int TotalPages { get; set; }
		public int PageNumber { get; set; }

		public PageList(int totalItems, int pageSize, int pageNumber, List<T> items)
		{
			this.TotalItems = totalItems;
			this.PageNumber = pageNumber;
			this.PageSize = pageSize;
			this.TotalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
			this.AddRange(items);
		}

		public static async Task<PageList<T>> GetPage(IQueryable<T> source, int pageNumber, int pageSize)
		{
			var totalItems = await source.CountAsync();
			var items = await source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
			return new PageList<T>(totalItems, pageSize, pageNumber, items);
		}
	}
}
