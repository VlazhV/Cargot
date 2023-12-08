using Ship.Domain.Entities;
using Ship.Domain.Interfaces;
using Ship.Infrastructure.Data;

namespace Ship.Infrastructure.Repositories;

public class RouteStopRepository : RepositoryBase<RouteStop, long>, IRouteStopRepository
{
    public RouteStopRepository(DatabaseContext db) : base(db)
    {
    }

}
