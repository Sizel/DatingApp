using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatingApp.Data.DTOs.Photos
{
	public class PhotoToReturnDTO
	{
		public int PhotoId { get; set; }
		public string Url { get; set; }
		public bool IsMain { get; set; }
		public DateTime DateAdded { get; set; }
	}
}
