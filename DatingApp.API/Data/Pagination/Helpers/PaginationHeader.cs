using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatingApp.Data.Pagination
{
	public class PaginationHeader
	{
		public int TotalItems { get; set; }
		public int PageSize { get; set; }
		public int TotalPages { get; set; }
		public int PageNumber { get; set; }

		public PaginationHeader(int totalItems, int pageSize, int totalPages, int pageNumber)
		{
			this.TotalItems = totalItems;
			this.PageSize = pageSize;
			this.TotalPages = totalPages;
			this.PageNumber = pageNumber;
		}
	}
}
