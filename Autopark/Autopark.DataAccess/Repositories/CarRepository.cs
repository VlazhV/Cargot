using Autopark.DataAccess.Data;
using Autopark.DataAccess.Entities;
using Autopark.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Autopark.DataAccess.Repositories;

public class CarRepository : RepositoryBase<Car>, ICarRepository
{
	public CarRepository(DatabaseContext db) : base(db)
	{
	}

	public bool DoesItExist(int id)
	{
		return _db.Cars
			.AsNoTracking()
			.Any(c => c.Id == id);
	}

	public bool DoesItExist(string licenseNumber)
	{
		return _db.Cars
			.AsNoTracking()
			.Any(c => c.LicenseNumber.Equals(licenseNumber));
	}

	public async Task<IEnumerable<Car>> GetWithSpecsAsync(IEnumerable<ISpecification<Car>> specs)
	{
		IQueryable<Car> query = _db.Cars
			.AsNoTracking()
			.Include(c => c.Schedules)
			.Include(c => c.Autopark);
			
		foreach (var s in specs)
		{
			query = s.Build(query);
		}

		var entities = await query.ToListAsync();

		return entities;
	}
}
