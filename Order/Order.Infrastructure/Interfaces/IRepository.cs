namespace Order.Infrastructure.Interfaces;

public interface IRepository<T, K>
{
	Task<T?> GetByIdAsync(K id);
	Task<IEnumerable<T>> GetAllAsync();
	Task<T> CreateAsync(T entity);
	Task<T> UpdateAsync(T entity);
	Task DeleteAsync(T entity);
	
}
