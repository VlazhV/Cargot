using Autopark.DataAccess.Entities;

namespace Autopark.DataAccess.Interfaces;

public interface ICarRepository: IRepository<Car>
{
	bool DoesItExist(string licenseNumber);
	Task<IEnumerable<Car>> GetWithSpecsAsync(IEnumerable<ISpecification<Car>> specs);
}
