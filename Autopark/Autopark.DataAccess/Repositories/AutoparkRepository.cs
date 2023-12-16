using Autopark.DataAccess.Data;
using Autopark.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Autopark.DataAccess.Repositories;

public class AutoparkRepository : RepositoryBase<Entities.Autopark>, IAutoparkRepository
{
	public AutoparkRepository(DatabaseContext db) : base(db)
	{
	}

	public async Task<bool> DoesItExistAsync(int id)
	{
		return await _db.Autoparks
			.AsNoTracking()
			.AnyAsync(a => a.Id == id);
	}

	public override async Task<IEnumerable<Entities.Autopark>> GetAllAsync()
	{
		return await _db.Autoparks
			.Include(a => a.Cars)
			.Include(a => a.Trailers)
			.ToListAsync();
	}

	public override async Task<Entities.Autopark?> GetByIdAsync(int id)
	{
		return await _db.Autoparks
			.Include(a => a.Cars)
			.Include(a => a.Trailers)
			.FirstOrDefaultAsync(a => a.Id == id);			
	}
}
