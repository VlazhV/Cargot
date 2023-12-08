using Ship.Domain.Interfaces;
using Ship.Infrastructure.Data;

namespace Ship.Infrastructure.Repositories;

public class ShipRepository : RepositoryBase<Domain.Entities.Ship, long>, IShipRepository
{
    public ShipRepository(DatabaseContext db) : base(db)
    {
    }
}
