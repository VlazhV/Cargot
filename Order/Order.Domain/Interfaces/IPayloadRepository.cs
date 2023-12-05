using Order.Domain.Entities;

namespace Order.Domain.Interfaces;

public interface IPayloadRepository: IRepository<Payload, long>
{
	Task<bool> IsPayloadExistsAsync(long id);
	Task CreateManyAsync(IEnumerable<Payload> payloads);
}
