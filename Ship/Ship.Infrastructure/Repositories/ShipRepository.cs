using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;
using MongoDB.Driver;
using Ship.Domain.Interfaces;
using Ship.Infrastructure.Data;

namespace Ship.Infrastructure.Repositories;

public class ShipRepository : RepositoryBase<Domain.Entities.Ship, ObjectId>, IShipRepository
{
	public ShipRepository(DatabaseContext db) : base(db)
	{
	}

	public Task<bool> IsShipExists(ObjectId id, CancellationToken cancellationToken)
	{
		return _db.Ships
			.AsNoTracking()
			.AnyAsync(ship => ship.Id == id, cancellationToken);
	}
}
