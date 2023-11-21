using Order.Domain.Entities;

namespace Order.Infrastructure.Interfaces;

public interface IPayloadRepository: IRepository<Payload, long>
{
}
