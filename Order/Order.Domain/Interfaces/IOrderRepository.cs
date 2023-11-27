namespace Order.Domain.Interfaces;

public interface IOrderRepository: IRepository<Domain.Entities.Order, long>
{
	Task<Domain.Entities.Order?> SetStatusAsync(Domain.Entities.Order order, string status);
	Task ClearPayloadListAsync(Domain.Entities.Order order);
}