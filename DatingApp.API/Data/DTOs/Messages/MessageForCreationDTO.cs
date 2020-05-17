using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatingApp.Data.DTOs.Messages
{
	public class MessageForCreationDTO
	{
		public int RecipientId { get; set; }
		public string Content { get; set; }
	}
}
