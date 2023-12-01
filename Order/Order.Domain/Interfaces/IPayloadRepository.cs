using Order.Domain.Entities;

namespace Order.Domain.Interfaces;

public interface IPayloadRepository: IRepository<Payload, long>
{
	bool DoesItExist(long id);
}
