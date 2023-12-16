using Autopark.DataAccess.Entities;

namespace Autopark.DataAccess.Interfaces;

public interface ICarRepository: IRepository<Car>
{
	Task<bool> DoesItExistAsync(int id);
	Task<bool> DoesItExistAsync(string licenseNumber);
	Task<IEnumerable<Car>> GetWithSpecsAsync(IEnumerable<ISpecification<Car>> specs);
}