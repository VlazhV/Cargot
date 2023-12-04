using Microsoft.EntityFrameworkCore;
using Order.Domain.Entities;
using Order.Infrastructure.Data;
using Order.Domain.Interfaces;

namespace Order.Infrastructure.Repositories;

public class PayloadRepository: RepositoryBase<Payload, long>, IPayloadRepository
{	
	
	public PayloadRepository(DatabaseContext db): base(db)
	{
	}

	public async Task<bool> DoesItExistAsync(long id)
	{
		return await _db.Payloads
			.AsNoTracking()
			.AnyAsync(p => p.Id == id);
	}
}
