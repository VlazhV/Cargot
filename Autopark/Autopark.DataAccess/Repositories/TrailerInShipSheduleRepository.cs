using Autopark.DataAccess.Data;
using Autopark.DataAccess.Entities;
using Autopark.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Autopark.DataAccess.Repositories;

public class TrailerInShipSheduleRepository : ITrailerInShipSheduleRepository
{
	private readonly DatabaseContext _db;
	
	public TrailerInShipSheduleRepository(DatabaseContext db)
	{
		_db = db;
	}
	
	public async Task<TrailerInShipShedule> CreateAsync(TrailerInShipShedule entity)
	{
		var entry = await _db.TrailerShedule.AddAsync(entity);
		await _db.SaveChangesAsync();

		return entry.Entity;		
	}

	public async Task DeleteAsync(TrailerInShipShedule entity)
	{
		_db.TrailerShedule.Remove(entity);
		await _db.SaveChangesAsync();
	}

	public bool DoesItExist(int id)
	{
		return _db.TrailerShedule.Any(s => s.Id == id);
	}

	public async Task<IEnumerable<TrailerInShipShedule>> GetAllAsync()
	{
		var entities = await _db.TrailerShedule.ToListAsync();

		return entities;
	}

	public async Task<IEnumerable<TrailerInShipShedule>> GetAllOfTrailerAsync(int trailerId)
	{
		var entities = await _db.TrailerShedule
			.Where(s => s.TrailerId == trailerId)
			.ToListAsync();

		return entities;
	}

	public async Task<TrailerInShipShedule?> GetByIdAsync(int id)
	{
		var entity = await _db.TrailerShedule
			.FirstOrDefaultAsync(s => s.Id == id);

		return entity;
	}

	public async Task<TrailerInShipShedule> UpdateAsync(TrailerInShipShedule entity)
	{
		var entry = _db.TrailerShedule.Update(entity);
		await _db.SaveChangesAsync();

		return entry.Entity;		
	}

}