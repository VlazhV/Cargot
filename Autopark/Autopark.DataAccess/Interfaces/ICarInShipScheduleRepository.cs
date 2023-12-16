using Autopark.DataAccess.Entities;

namespace Autopark.DataAccess.Interfaces;

public interface ICarInShipScheduleRepository: IRepository<CarInShipSchedule>
{
	Task<bool> DoesItExistAsync(int id);
	Task<IEnumerable<CarInShipSchedule>> GetAllOfCarAsync(int carId);
}