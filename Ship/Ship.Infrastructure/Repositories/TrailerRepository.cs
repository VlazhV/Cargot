using Ship.Domain.Entities;
using Ship.Domain.Interfaces;
using Ship.Infrastructure.Data;

namespace Ship.Infrastructure.Repositories;

public class TrailerRepository : RepositoryBase<Trailer, int>, ITrailerRepository
{
    public TrailerRepository(DatabaseContext db) : base(db)
    {
    }
}
