using System.Linq.Expressions;

namespace HealthTea.Data.Base
{
	public interface IBaseEntityRepository<T> where T : class, IBaseEntity, new()
	{
		Task<T> GetByIdAsync(int id);
		Task<T> GetByIdAsync(int id, params Expression<Func<T, object>>[] includeProperties);
		Task<IEnumerable<T>> GetAllAsync();
		Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includeProperties);
		Task AddAsync(T entity);
		Task UpdateAsync(int id, T entity);
		Task DeleteAsync(int id);
	}
}
