using DatingApp.API.Data;
using DatingApp.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatingApp.Data.Repos
{
	public class PhotoRepository : Repository<Photo>, IPhotoRepository
	{
		public PhotoRepository(DataContext context) : base(context) { }
	}
}
