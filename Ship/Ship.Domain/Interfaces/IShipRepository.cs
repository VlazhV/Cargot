namespace Ship.Domain.Interfaces;

public interface IShipRepository: IRepository<Entities.Ship, long>
{
    Task<bool> IsShipExists(long id);
}
