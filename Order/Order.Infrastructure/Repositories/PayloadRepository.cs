using Microsoft.EntityFrameworkCore;
using Order.Domain.Entities;
using Order.Infrastructure.Data;
using Order.Infrastructure.Interfaces;

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
		await _db.SaveChangesAsync();
		
		return entry.Entity;
	}

	public async Task DeleteAsync(Payload entity)
	{
		_db.Payloads.Remove(entity);
		await _db.SaveChangesAsync();
	}

	public async Task<IEnumerable<Payload>> GetAllAsync()
	{
		return await _db.Payloads
			.Include(p => p.Order)
			.ToListAsync();
	}

	public async Task<Payload?> GetByIdAsync(long id)
	{
		return await _db.Payloads
			.Include(p => p.Order)
			.FirstOrDefaultAsync();
	}

	public async Task<Payload> UpdateAsync(Payload entity)
	{
		var entry = _db.Payloads.Update(entity);
		await _db.SaveChangesAsync();

		return entry.Entity;
	}

}
