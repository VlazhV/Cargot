using Autopark.DataAccess.Entities;

namespace Autopark.DataAccess.Interfaces;

public interface ICarInShipScheduleRepository: IRepository<CarInShipSchedule>
{
	bool DoesItExist(int id);
	Task<IEnumerable<CarInShipSchedule>> GetAllOfCarAsync(int carId);
}