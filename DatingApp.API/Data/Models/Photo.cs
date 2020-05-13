using DatingApp.API.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatingApp.Data.Models
{
	public class Photo
	{
		public int PhotoId { get; set; }
		public string Url { get; set; }
		public string Description { get; set; }
		public DateTime DateAdded { get; set; }
		public Boolean IsMain { get; set; }
		public int UserId { get; set; }
	}
}
