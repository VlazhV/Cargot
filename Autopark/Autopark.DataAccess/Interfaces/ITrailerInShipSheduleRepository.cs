using Autopark.DataAccess.Entities;

namespace Autopark.DataAccess.Interfaces;

public interface ITrailerInShipSheduleRepository: IRepository<TrailerInShipShedule>
{
    Task<IEnumerable<TrailerInShipShedule>> GetAllOfTrailerAsync(int trailerId);
}