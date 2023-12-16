using Autopark.DataAccess.Entities;

namespace Autopark.DataAccess.Interfaces;

public interface ITrailerInShipScheduleRepository: IRepository<TrailerInShipSchedule>
{
    Task<bool> DoesItExistAsync(int id);
	Task<IEnumerable<TrailerInShipSchedule>> GetAllOfTrailerAsync(int trailerId);
}