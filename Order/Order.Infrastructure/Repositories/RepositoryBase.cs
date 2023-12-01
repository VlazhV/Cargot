using Microsoft.EntityFrameworkCore;
using Order.Domain.Interfaces;
using Order.Infrastructure.Data;

namespace Order.Infrastructure.Repositories;

public abstract class RepositoryBase<T, K>: IRepository<T, K> where T: class
{
	protected readonly DatabaseContext _db;
	
	public RepositoryBase(DatabaseContext db)
	{
		_db = db;
	}
	
	public async Task<T> CreateAsync(T entity)
	{
		var entry = await _db.AddAsync(entity!);

		return entry.Entity;
	}

	public void Delete(T entity)
	{
		_db.Remove(entity!);
	}

	public async Task<IEnumerable<T>> GetAllAsync()
	{
		return await _db.Set<T>().AsNoTracking().ToListAsync();
	}

	public async Task<T?> GetByIdAsync(K id)
	{
		return await _db.Set<T>().FindAsync(id);
	}

	public async Task SaveChangesAsync()
	{
		await _db.SaveChangesAsync();
	}

	public T Update(T entity)
	{
		var entry = _db.Update(entity);

		return entry.Entity;
	}

}