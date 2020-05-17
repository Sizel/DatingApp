using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatingApp.Data.Pagination
{
	public class PaginationParams
	{
		public const int MaxPageSize = 50;
		public int PageNumber { get; set; } = 1;

		private int pageSize = 10;
		public int PageSize
		{
			get { return pageSize; }
			set { pageSize = (pageSize > MaxPageSize) ? MaxPageSize : value; }
		}
	}
}
