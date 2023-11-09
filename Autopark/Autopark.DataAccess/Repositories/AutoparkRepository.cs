using Autopark.DataAccess.Interfaces;

namespace Autopark.DataAccess.Repositories;

public class AutoparkRepository : IAutoparkRepository
{
    public Task<Entities.Autopark> CreateAsync(Entities.Autopark entity)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(Entities.Autopark entity)
    {
        throw new NotImplementedException();
    }

    public Task<Entities.Autopark> Get(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Entities.Autopark>> GetAll(Entities.Autopark entity)
    {
        throw new NotImplementedException();
    }

    public Task<Entities.Autopark> UpdateAsync(Entities.Autopark entity)
    {
        throw new NotImplementedException();
    }

}
