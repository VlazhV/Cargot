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

	public async Task<bool> IsUserExistsAsync(long id)
	{
		return await _db.Users
			.AsNoTracking()
			.AnyAsync(u => u.Id == id);
	}

	public async Task<bool> IsUserExistsAsync(User user)
	{
		return await _db.Users
			.AsNoTracking()
			.AnyAsync(u => u.Email.Equals(user.Email) || u.PhoneNumber.Equals(user.PhoneNumber) || u.UserName.Equals(user.UserName));
	}
}
