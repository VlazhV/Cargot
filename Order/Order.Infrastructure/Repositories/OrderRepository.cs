using Microsoft.EntityFrameworkCore;
using Order.Infrastructure.Data;
using Order.Infrastructure.Interfaces;

namespace Order.Infrastructure.Repositories;

public class OrderRepository : IOrderRepository
{
	private readonly DatabaseContext _db;
	
	public OrderRepository(DatabaseContext db)
	{
		_db = db;
	}
	
	public async Task<Domain.Entities.Order> CreateAsync(Domain.Entities.Order entity)
	{
		var entry = await _db.Orders.AddAsync(entity);
		await _db.SaveChangesAsync();
		return entry.Entity;
	}

	public async Task DeleteAsync(Domain.Entities.Order entity)
	{
		_db.Orders.Remove(entity);
		await _db.SaveChangesAsync();
	}

	public async Task<IEnumerable<Domain.Entities.Order>> GetAllAsync()
	{
		return await _db.Orders
			.Include(o => o.Payloads)
			.Include(o => o.Client)
			.Include(o => o.OrderStatus)
			.ToListAsync();
	}

	public async Task<Domain.Entities.Order?> GetByIdAsync(long id)
	{
		return await _db.Orders
			.Include(o => o.Payloads)
			.Include(o => o.Client)
			.Include(o => o.OrderStatus)
			.FirstOrDefaultAsync(o => o.Id == id);
	}

	public async Task<Domain.Entities.Order> UpdateAsync(Domain.Entities.Order entity)
	{
		var entry = _db.Orders.Update(entity);
		await _db.SaveChangesAsync();
		return entry.Entity;
	}

}
