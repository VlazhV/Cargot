using System;
namespace Ship.Domain.Interfaces;

public interface IRepository<T, K>
{
	Task<T?> GetByIdAsync(K id);
	Task<IEnumerable<T>> GetAllAsync(IEnumerable<ISpecification<T>> specs);
	Task<T> CreateAsync(T entity);
	T Update(T entity);
	void Delete(T entity);
	Task SaveChangesAsync();
}
