using Ship.Domain.Entities;
using Ship.Domain.Interfaces;
using Ship.Infrastructure.Data;

namespace Ship.Infrastructure.Repositories;

public class CarRepository : RepositoryBase<Car, int>, ICarRepository
{
    public CarRepository(DatabaseContext db) : base(db)
    {
    }
}
