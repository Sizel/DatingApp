namespace DatingApp.Data.Pagination
{
	public class MessagePaginationParams : PaginationParams
	{
		public string MessageType { get; set; } = "Unread";
	}
}
