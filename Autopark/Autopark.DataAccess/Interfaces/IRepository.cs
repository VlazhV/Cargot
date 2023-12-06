namespace Autopark.DataAccess.Interfaces;

public interface IRepository<T>
{
	Task<IEnumerable<T>> GetAllAsync();
	Task<T?> GetByIdAsync(int id);
	Task<T> CreateAsync(T entity);
	void Delete(T entity);
	T Update(T entity);
	Task SaveChangesAsync();
}
