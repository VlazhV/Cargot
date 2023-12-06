using Autopark.DataAccess.Entities;

namespace Autopark.DataAccess.Interfaces;

public interface ITrailerInShipScheduleRepository: IRepository<TrailerInShipSchedule>
{
    bool DoesItExist(int id);
	Task<IEnumerable<TrailerInShipSchedule>> GetAllOfTrailerAsync(int trailerId);
}