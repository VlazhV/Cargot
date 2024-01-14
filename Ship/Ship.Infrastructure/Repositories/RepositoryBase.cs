using Microsoft.EntityFrameworkCore;
using Ship.Domain.Interfaces;
using Ship.Infrastructure.Data;

namespace Ship.Infrastructure.Repositories;

public class RepositoryBase<T, K> : IRepository<T, K> where T : class
{
	protected readonly DatabaseContext _db;
	
	public RepositoryBase(DatabaseContext db)
	{
		_db = db;
	}
	
	public async Task<T> CreateAsync(T entity, CancellationToken cancellationToken)
	{
		var entry = await _db.AddAsync(entity!, cancellationToken);

		return entry.Entity;
	}

	public void Delete(T entity)
	{
		_db.Remove(entity!);
	}

	public async Task<IEnumerable<T>> GetAllAsync(IEnumerable<ISpecification<T>> specs, CancellationToken cancellationToken)
	{
		var query = _db.Set<T>().AsNoTracking();
		
		foreach (var spec in specs)
		{
			query = spec.Build(query);
		}
		
		return await query.ToListAsync(cancellationToken);
    }

	public async Task<T?> GetByIdAsync(K id, CancellationToken cancellationToken)
	{
		return await _db.Set<T>().FindAsync(id, cancellationToken);
	}

	public async Task SaveChangesAsync(CancellationToken cancellationToken)
	{
		await _db.SaveChangesAsync(cancellationToken);
	}

	public T Update(T entity)
	{
		var entry = _db.Update(entity);

		return entry.Entity;
	}
}
