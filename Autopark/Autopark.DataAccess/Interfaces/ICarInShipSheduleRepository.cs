using Autopark.DataAccess.Entities;

namespace Autopark.DataAccess.Interfaces;

public interface ICarInShipSheduleRepository: IRepository<CarInShipShedule>
{
    Task<IEnumerable<CarInShipShedule>> GetAllOfCarAsync(int carId);    
}