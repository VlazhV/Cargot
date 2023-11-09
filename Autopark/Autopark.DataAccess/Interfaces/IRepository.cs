namespace Autopark.DataAccess.Interfaces;

public interface IRepository<T>
{
	Task<IEnumerable<T>> GetAll(T entity);
	Task<T> Get(int id);
	Task<T> CreateAsync(T entity);
	Task DeleteAsync(T entity);
	Task<T> UpdateAsync(T entity);	
}
