using Autopark.DataAccess.Entities;
using Autopark.DataAccess.Interfaces;

namespace Autopark.DataAccess.Repositories;

public class TrailerRepository : ITrailerRepository
{
    public Task<Trailer> CreateAsync(Trailer entity)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(Trailer entity)
    {
        throw new NotImplementedException();
    }

    public Task<Trailer> Get(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Trailer>> GetAll(Trailer entity)
    {
        throw new NotImplementedException();
    }

    public Task<Trailer> UpdateAsync(Trailer entity)
    {
        throw new NotImplementedException();
    }

}
