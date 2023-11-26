using Autopark.DataAccess.Data;
using Autopark.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace Autopark.DataAccess.Repositories;

public class CarInShipScheduleRepository : RepositoryBase<CarInShipSchedule>
{
	public CarInShipScheduleRepository(DatabaseContext db) : base(db)
	{
	}

	public bool DoesItExist(int id)
	{
		return _db.CarSchedule
			.AsNoTracking()
			.Any(s => s.Id == id);
	}

	public async Task<IEnumerable<CarInShipSchedule>> GetAllOfCarAsync(int carId)
	{
		var entities = await _db.CarSchedule
			.AsNoTracking()
			.Where(s => s.CarId == carId)
			.ToListAsync();

		return entities;
	}
}