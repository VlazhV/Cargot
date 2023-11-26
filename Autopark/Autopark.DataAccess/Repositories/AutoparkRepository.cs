using Autopark.DataAccess.Data;
using Microsoft.EntityFrameworkCore;

namespace Autopark.DataAccess.Repositories;

public class AutoparkRepository : RepositoryBase<Entities.Autopark>
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
