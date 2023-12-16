using Autopark.DataAccess.Entities;

namespace Autopark.DataAccess.Interfaces;

public interface ITrailerRepository: IRepository<Trailer>
{
	Task<bool> DoesItExistAsync(int id);
	Task<bool> DoesItExistAsync(string licenseNumber);
	Task<IEnumerable<Trailer>> GetWithSpecsAsync(IEnumerable<ISpecification<Trailer>> specs);
}