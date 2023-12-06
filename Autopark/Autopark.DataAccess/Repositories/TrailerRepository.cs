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

	public bool DoesItExist(int id)
	{
		return _db.Trailers
			.AsNoTracking()
			.Any(t => t.Id == id);
	}

	public bool DoesItExist(string licenseNumber)
	{
		return _db.Trailers
			.AsNoTracking()
			.Any(t => t.LicenseNumber.Equals(licenseNumber));
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
