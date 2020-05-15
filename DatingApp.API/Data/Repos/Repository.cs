using DatingApp.API.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatingApp.Data
{
	public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
	{
		protected readonly DataContext Context;

		public Repository(DataContext context)
		{
			Context = context;
		}

		public async void Add(TEntity entity)
		{
			await Context.Set<TEntity>().AddAsync(entity);
		}

		public async Task<TEntity> Get(int id)
		{
			return await Context.Set<TEntity>().FindAsync(id);
		}

		public async Task<IEnumerable<TEntity>> GetAll()
		{
			return await Context.Set<TEntity>().ToListAsync<TEntity>();
		}

		public void Remove(TEntity entity)
		{
			Context.Set<TEntity>().Remove(entity);
		}

		public async Task<bool> SaveAll()
		{
			return await Context.SaveChangesAsync() > 0;
		}
	}
}
