using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatingApp.Data.Pagination
{
	public class MessagePaginationParams : PaginationParams
	{
		public string MessageType { get; set; } = "Unread";
	}
}
