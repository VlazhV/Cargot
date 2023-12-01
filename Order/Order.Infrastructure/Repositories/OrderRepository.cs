using Microsoft.EntityFrameworkCore;
using Order.Infrastructure.Data;
using Order.Domain.Interfaces;

namespace Order.Infrastructure.Repositories;

public class OrderRepository: RepositoryBase<Domain.Entities.Order, long>, IOrderRepository
{
	public OrderRepository(DatabaseContext db): base(db)
	{
	}
	
	public void ClearPayloadList(Domain.Entities.Order order)
	{
		_db.Payloads.RemoveRange(order.Payloads);				
	}

	public bool DoesItExist(long id)
	{
		return _db.Orders
			.AsNoTracking()
			.Any(o => o.Id == id);
	}

	public async Task<Domain.Entities.Order?> SetStatusAsync(Domain.Entities.Order order, string status)
	{
		var statusEntity = await _db.OrderStatuses.FirstOrDefaultAsync(s => s.Name.Equals(status));

		if (statusEntity is null)
			return null;

		order.OrderStatus = statusEntity;
		var entry = _db.Orders.Update(order);

		return entry.Entity;
	}
}
