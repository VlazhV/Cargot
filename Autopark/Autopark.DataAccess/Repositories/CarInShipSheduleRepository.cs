using Autopark.DataAccess.Data;
using Autopark.DataAccess.Entities;
using Autopark.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Autopark.DataAccess.Repositories;

public class CarInShipScheduleRepository : RepositoryBase<CarInShipSchedule>, ICarInShipScheduleRepository
{
	public CarInShipScheduleRepository(DatabaseContext db) : base(db)
	{
	}

	public async Task<bool> DoesItExistAsync(int id)
	{
		return await _db.CarSchedule
			.AsNoTracking()
			.AnyAsync(s => s.Id == id);
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