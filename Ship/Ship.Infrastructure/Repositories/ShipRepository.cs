using Microsoft.EntityFrameworkCore;
using Ship.Domain.Interfaces;
using Ship.Infrastructure.Data;

namespace Ship.Infrastructure.Repositories;

public class ShipRepository : RepositoryBase<Domain.Entities.Ship, long>, IShipRepository
{
	public ShipRepository(DatabaseContext db) : base(db)
	{
	}

	public Task<bool> IsShipExists(long id)
	{
		return _db.Ships
			.AsNoTracking()
			.AnyAsync(ship => ship.Id == id);
	}
}
