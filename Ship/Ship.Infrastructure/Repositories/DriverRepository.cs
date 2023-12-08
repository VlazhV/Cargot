using Ship.Domain.Entities;
using Ship.Domain.Interfaces;
using Ship.Infrastructure.Data;

namespace Ship.Infrastructure.Repositories;

public class DriverRepository : RepositoryBase<Driver, int>, IDriverRepository
{
    public DriverRepository(DatabaseContext db) : base(db)
    {
    }

}
