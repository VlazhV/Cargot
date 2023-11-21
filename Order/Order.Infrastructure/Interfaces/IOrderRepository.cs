namespace Order.Infrastructure.Interfaces;

public interface IOrderRepository: IRepository<Domain.Entities.Order, long>
{
}
