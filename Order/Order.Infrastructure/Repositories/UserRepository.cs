using Microsoft.EntityFrameworkCore;
using Order.Domain.Entities;
using Order.Infrastructure.Data;
using Order.Domain.Interfaces;

namespace Order.Infrastructure.Repositories;

public class UserRepository: RepositoryBase<User, long>, IUserRepository
{
	public UserRepository(DatabaseContext db): base(db)
	{		
	}

	public bool DoesItExist(long id)
	{
		return _db.Users
			.AsNoTracking()
			.Any(u => u.Id == id);
	}

	public bool DoesItExist(User user)
	{
		return _db.Users
			.AsNoTracking()
			.Any(u => u.Email.Equals(user.Email) || u.PhoneNumber.Equals(user.PhoneNumber) || u.UserName.Equals(user.UserName));
	}
}
