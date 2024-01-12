using MongoDB.Bson;

namespace Ship.Domain.Interfaces;

public interface IShipRepository: IRepository<Entities.Ship, ObjectId>
{
    Task<bool> IsShipExists(ObjectId id);
}
