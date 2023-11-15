using Autopark.DataAccess.Data;
using Autopark.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Autopark.DataAccess.Repositories;

public class AutoparkRepository : IAutoparkRepository
{
	private readonly DatabaseContext _db;

	public AutoparkRepository(DatabaseContext db)
	{
		_db = db;
	}
	
	public async Task<Entities.Autopark> CreateAsync(Entities.Autopark entity)
	{
		var entry = await _db.Autoparks.AddAsync(entity);
		await _db.SaveChangesAsync();

		return entry.Entity;
	}

	public bool DoesItExist(int id)
	{
		return _db.Autoparks.Any(a => a.Id == id);
	}
	
	public async Task DeleteAsync(Entities.Autopark entity)
	{
		_db.Autoparks.Remove(entity);
		await _db.SaveChangesAsync();
	}

	public async Task<Entities.Autopark?> GetByIdAsync(int id)
	{
		var entity = await _db.Autoparks
			.Include(a => a.Cars)
			.Include(a => a.Trailers)
			.FirstOrDefaultAsync(a => a.Id == id);

		return entity;
	}

	public async Task<IEnumerable<Entities.Autopark>> GetAllAsync()
	{
		var entities = await _db.Autoparks
			.Include(a => a.Cars)
			.Include(a => a.Trailers)
			.ToListAsync();

		return entities;
	}

	public async Task<Entities.Autopark> UpdateAsync(Entities.Autopark entity)
	{
		var entry = _db.Autoparks.Update(entity);
		await _db.SaveChangesAsync();
		return entry.Entity;
	}

}
