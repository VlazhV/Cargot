using System;
namespace Ship.Domain.Interfaces;

public interface IRepository<T, K>
{
	Task<T?> GetByIdAsync(K id, CancellationToken cancellationToken);
	Task<IEnumerable<T>> GetAllAsync(IEnumerable<ISpecification<T>> specs, CancellationToken cancellationToken);
	Task<T> CreateAsync(T entity, CancellationToken cancellationToken);
	T Update(T entity);
	void Delete(T entity);
	Task SaveChangesAsync(CancellationToken cancellationToken);
}
