using Autopark.DataAccess.Data;
using Autopark.DataAccess.Entities;
using Autopark.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Autopark.DataAccess.Repositories;

public class CarInShipSheduleRepository : ICarInShipSheduleRepository
{
	private readonly DatabaseContext _db;
	
	public CarInShipSheduleRepository(DatabaseContext db)
	{
		_db = db;
	}
	
	public async Task<CarInShipShedule> CreateAsync(CarInShipShedule entity)
	{
		var entry = await _db.CarShedule.AddAsync(entity);
		await _db.SaveChangesAsync();

		return entry.Entity;
	}

	public async Task DeleteAsync(CarInShipShedule entity)
	{
		_db.CarShedule.Remove(entity);
		await _db.SaveChangesAsync();		
	}

	public bool DoesItExist(int id)
	{
		return _db.CarShedule.Any(s => s.Id == id);
	}

	public async Task<IEnumerable<CarInShipShedule>> GetAllAsync()
	{
		var entities = await _db.CarShedule.ToListAsync();

		return entities;
	}

	public async Task<IEnumerable<CarInShipShedule>> GetAllOfCarAsync(int carId)
	{
		var entities = await _db.CarShedule
			.Where(s => s.CarId == carId)
			.ToListAsync();

		return entities;
	}

	public async Task<CarInShipShedule?> GetByIdAsync(int id)
	{
		var entity = await _db.CarShedule.FirstOrDefaultAsync(s => s.Id == id);

		return entity;
	}

	public async Task<CarInShipShedule> UpdateAsync(CarInShipShedule entity)
	{
		var entry = _db.CarShedule.Update(entity);
		await _db.SaveChangesAsync();

		return entry.Entity;
	}

}