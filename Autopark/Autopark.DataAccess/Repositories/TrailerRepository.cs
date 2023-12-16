using Autopark.DataAccess.Data;
using Autopark.DataAccess.Entities;
using Autopark.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Autopark.DataAccess.Repositories;

public class TrailerRepository : RepositoryBase<Trailer>, ITrailerRepository
{
	public TrailerRepository(DatabaseContext db): base(db)
	{		
	}

	public async Task<bool> DoesItExistAsync(int id)
	{
		return await _db.Trailers
			.AsNoTracking()
			.AnyAsync(t => t.Id == id);
	}

	public async Task<bool> DoesItExistAsync(string licenseNumber)
	{
		return await _db.Trailers
			.AsNoTracking()
			.AnyAsync(t => t.LicenseNumber.Equals(licenseNumber));
	}

	public async Task<IEnumerable<Trailer>> GetWithSpecsAsync(IEnumerable<ISpecification<Trailer>> specs)
	{
		IQueryable<Trailer> query = _db.Trailers
			.AsNoTracking()
			.Include(t => t.Schedules)
			.Include(t => t.Autopark);
			
		foreach (var s in specs)
		{
			query = s.Build(query);
		}
			
		var entities = await query.ToListAsync();

		return entities;
	}
}
