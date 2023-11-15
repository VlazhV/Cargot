using Autopark.DataAccess.Data;
using Autopark.DataAccess.Entities;
using Autopark.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Autopark.DataAccess.Repositories;

public class TrailerRepository : ITrailerRepository
{
	private readonly DatabaseContext _db;
	
	public TrailerRepository(DatabaseContext db)
	{
		_db = db;
	}
	
	public async Task<Trailer> CreateAsync(Trailer entity)
	{
		var entry = await _db.Trailers.AddAsync(entity);
		await _db.SaveChangesAsync();
		
		return entry.Entity;
	}

	public bool DoesItExist(int id)
	{
		return _db.Trailers.Any(t => t.Id == id);
	}
	
	public async Task DeleteAsync(Trailer entity)
	{
		_db.Trailers.Remove(entity);
		await _db.SaveChangesAsync();
	}

	public async Task<Trailer?> GetByIdAsync(int id)
	{
		var entity = await _db.Trailers
			.Include(t => t.Autopark)
			.FirstOrDefaultAsync(t => t.Id == id);

		return entity;
	}

	public async Task<IEnumerable<Trailer>> GetAllAsync()
	{
		var entities = await _db.Trailers
			.Include(t => t.Autopark)
			.ToListAsync();

		return entities;
	}

	public async Task<Trailer> UpdateAsync(Trailer entity)
	{
		var entry = _db.Trailers.Update(entity);
		await _db.SaveChangesAsync();

		return entry.Entity;
	}

}
