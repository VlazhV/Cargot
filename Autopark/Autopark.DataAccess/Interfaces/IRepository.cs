namespace Autopark.DataAccess.Interfaces;

public interface IRepository<T>
{
	Task<IEnumerable<T>> GetAllAsync();
	Task<T?> GetByIdAsync(int id);
	Task<T> CreateAsync(T entity);
	Task DeleteAsync(T entity);
	Task<T> UpdateAsync(T entity);
	bool DoesItExist(int id);
}
