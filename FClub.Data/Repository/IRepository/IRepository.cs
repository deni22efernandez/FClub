using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace FClub.Data.Repository.IRepository
{
	public interface IRepository<T> where T : class
	{
		Task CreateAsync(T entity);
		Task DeleteAsync(T entity);
		Task<IEnumerable<T>> GetAllAync(Expression<Func<T, bool>> filter = null, string includeProperties = null);
		Task<T> GetAync(Expression<Func<T, bool>> filter = null, string includeProperties = null);
	}
}