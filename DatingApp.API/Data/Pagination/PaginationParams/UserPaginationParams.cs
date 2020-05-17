using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatingApp.Data.Pagination
{
	public class UserPaginationParams : PaginationParams
	{
		public string Gender { get; set; }
		public int MinAge { get; set; } = 0;
		public int MaxAge { get; set; } = 99;
		public string OrderBy { get; set; } = "lastActive";
		public bool Likers { get; set; } = false;
		public bool Likees { get; set; } = false;

	}
}
