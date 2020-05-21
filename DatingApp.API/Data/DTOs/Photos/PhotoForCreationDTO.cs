using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatingApp.Data.DTOs.Photos
{
	public class PhotoForCreationDTO
	{
		public IFormFile File { get; set; }
	}
}
