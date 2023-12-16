using Autopark.DataAccess.Data;
using Autopark.DataAccess.Entities;
using Autopark.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Autopark.DataAccess.Repositories;

public class TrailerInShipScheduleRepository : RepositoryBase<TrailerInShipSchedule>, ITrailerInShipScheduleRepository
{
	public TrailerInShipScheduleRepository(DatabaseContext db): base(db)
	{
	}
	
	public async Task<bool> DoesItExistAsync(int id)
	{
		return await _db.TrailerSchedule
			.AsNoTracking()
			.AnyAsync(s => s.Id == id);
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