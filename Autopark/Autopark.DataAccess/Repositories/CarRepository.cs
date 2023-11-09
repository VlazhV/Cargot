using Autopark.DataAccess.Entities;
using Autopark.DataAccess.Interfaces;

namespace Autopark.DataAccess.Repositories;

public class CarRepository : ICarRepository
{
    public Task<Car> CreateAsync(Car entity)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(Car entity)
    {
        throw new NotImplementedException();
    }

    public Task<Car> Get(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Car>> GetAll(Car entity)
    {
        throw new NotImplementedException();
    }

    public Task<Car> UpdateAsync(Car entity)
    {
        throw new NotImplementedException();
    }

}
