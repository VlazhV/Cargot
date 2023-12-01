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

	public bool DoesItExist(long id)
	{
		return _db.Payloads
			.AsNoTracking()
			.Any(p => p.Id == id);
	}
}
