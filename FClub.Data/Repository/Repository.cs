using FClub.Data.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace FClub.Data.Repository
{
	public class Repository<T> : IRepository<T> where T : class
	{
		private readonly ApplicationDbContext _context;
		internal DbSet<T> _dbSet;
		public Repository(ApplicationDbContext context)
		{
			_context = context;
			_dbSet = _context.Set<T>();
		}
		
		public async Task<T> GetAsync(Expression<Func<T, bool>> filter = null, string includeProperties = null)
		{
			IQueryable<T> query = _dbSet;
			if (filter != null)
			{
				query = query.Where(filter);
			}
			if (!String.IsNullOrEmpty(includeProperties))
			{
				foreach (var item in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
				{
					query = query.Include(item);
				}
			}
			return await query.FirstOrDefaultAsync();
			
		}
		public async Task<IEnumerable<T>> GetAllAync(Expression<Func<T, bool>> filter = null, string includeProperties = null)
		{
			IQueryable<T> query = _dbSet;
			if (filter != null)
			{
				query = query.Where(filter);
			}
			if (!String.IsNullOrEmpty(includeProperties))
			{
				foreach (var item in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
				{
					query = query.Include(item);
				}
			}
			var result = await query.ToListAsync();
			return result;
		}
		public async Task CreateAsync(T entity)
		{
			 await _dbSet.AddAsync(entity);
		}
		public async Task DeleteAsync(T entity)
		{
			var entt = await _dbSet.FindAsync(entity);
			if (entt != null)
			{
				_dbSet.Remove(entt);				
			}
			
		}
	}
}
