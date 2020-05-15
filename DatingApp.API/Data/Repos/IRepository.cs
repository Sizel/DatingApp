using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatingApp.Data
{
	public interface IRepository<TEntity> where TEntity : class
	{
		Task<TEntity> Get(int id);
		Task<IEnumerable<TEntity>> GetAll();
		void Add(TEntity entity);
		void Remove(TEntity entity);
		public Task<bool> SaveAll();
	}
}
