using Autopark.DataAccess.Data;
using Autopark.DataAccess.Entities;
using Autopark.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Autopark.DataAccess.Repositories;

public class CarRepository : ICarRepository
{
	private readonly DatabaseContext _db;

	public CarRepository(DatabaseContext db)
	{
		_db = db;
	}

	
	public async Task<Car> CreateAsync(Car entity)
	{
		var entry = await _db.Cars.AddAsync(entity);
		await _db.SaveChangesAsync();
		
		return entry.Entity;
	}

	public async Task DeleteAsync(Car entity)
	{
		_db.Cars.Remove(entity);
		await _db.SaveChangesAsync();
	}

	public async Task<Car?> GetByIdAsync(int id)
	{
		var entity = await _db.Cars
			.Include(c => c.Autopark)
			.FirstOrDefaultAsync(c => c.Id == id);

		return entity;
	}

	public async Task<IEnumerable<Car>> GetAllAsync()
	{
		var entities = await _db.Cars
			.Include(c => c.Autopark)
			.ToListAsync();

		return entities;
	}

	public async Task<Car> UpdateAsync(Car entity)
	{
		var entry = _db.Cars.Update(entity);
		await _db.SaveChangesAsync();

		return entry.Entity;
	}

	public bool DoesItExist(int id)
	{
		return _db.Cars.Any(c => c.Id == id);
	}

	public bool DoesItExist(string licenseNumber)
	{
		return _db.Cars.Any(c => c.LicenseNumber.Equals(licenseNumber));
	}

	public async Task<IEnumerable<Car>> GetWithSpecsAsync(IEnumerable<ISpecification<Car>> specs)
	{
		IQueryable<Car> query = _db.Cars.Include(c => c.Autopark);
		foreach (var s in specs)
			query = s.Build(query);

		var entities = await query.ToListAsync();

		return entities;
	}
}
