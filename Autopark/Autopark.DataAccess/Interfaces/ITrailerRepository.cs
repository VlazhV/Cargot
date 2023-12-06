using Autopark.DataAccess.Entities;

namespace Autopark.DataAccess.Interfaces;

public interface ITrailerRepository: IRepository<Trailer>
{
	bool DoesItExist(int id);
	bool DoesItExist(string licenseNumber);
	Task<IEnumerable<Trailer>> GetWithSpecsAsync(IEnumerable<ISpecification<Trailer>> specs);
}