using Autopark.DataAccess.Data;
using Autopark.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Autopark.DataAccess.Repositories;

public class AutoparkRepository : RepositoryBase<Entities.Autopark>, IAutoparkRepository
{
	public AutoparkRepository(DatabaseContext db) : base(db)
	{
	}

	public bool DoesItExist(int id)
	{
		return _db.Autoparks
			.AsNoTracking()
			.Any(a => a.Id == id);
	}
}
