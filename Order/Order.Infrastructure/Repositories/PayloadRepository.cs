using Microsoft.EntityFrameworkCore;
using Order.Domain.Entities;
using Order.Infrastructure.Data;
using Order.Domain.Interfaces;

namespace Order.Infrastructure.Repositories;

public class PayloadRepository : IPayloadRepository
{
	private readonly DatabaseContext _db;
	
	public PayloadRepository(DatabaseContext db)
	{
		_db = db;
	}
	
	public async Task<Payload> CreateAsync(Payload entity)
	{
		var entry = await _db.Payloads.AddAsync(entity);		
		
		return entry.Entity;
	}

	public void Delete(Payload entity)
	{
		_db.Payloads.Remove(entity);		
	}

	public bool DoesItExist(long id)
	{
		return _db.Payloads
			.AsNoTracking()
			.Any(p => p.Id == id);
	}

	public async Task<IEnumerable<Payload>> GetAllAsync()
	{
		return await _db.Payloads
			.Include(p => p.Order)
				.ThenInclude(o => o.OrderStatus)
			.AsNoTracking()
			.ToListAsync();
	}

	public async Task<Payload?> GetByIdAsync(long id)
	{
		return await _db.Payloads
			.Include(p => p.Order)
				.ThenInclude(o => o.OrderStatus)
			.AsNoTracking()
			.FirstOrDefaultAsync(p => p.Id == id);
	}

	public Payload Update(Payload entity)
	{
		var entry = _db.Payloads.Update(entity);		

		return entry.Entity;
	}

	public async Task SaveChangesAsync()
	{
		await _db.SaveChangesAsync();
	}
}
