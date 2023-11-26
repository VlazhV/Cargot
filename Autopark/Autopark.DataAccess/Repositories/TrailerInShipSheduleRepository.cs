using Autopark.DataAccess.Data;
using Autopark.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace Autopark.DataAccess.Repositories;

public class TrailerInShipScheduleRepository : RepositoryBase<TrailerInShipSchedule>
{
	public TrailerInShipScheduleRepository(DatabaseContext db): base(db)
	{
	}
	
	public bool DoesItExist(int id)
	{
		return _db.TrailerSchedule
			.AsNoTracking()
			.Any(s => s.Id == id);
	}

	public async Task<IEnumerable<TrailerInShipSchedule>> GetAllOfTrailerAsync(int trailerId)
	{
		var entities = await _db.TrailerSchedule
			.AsNoTracking()
			.Where(s => s.TrailerId == trailerId)
			.ToListAsync();

		return entities;
	}
}