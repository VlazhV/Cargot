using Microsoft.EntityFrameworkCore;
using Order.Domain.Entities;
using Order.Infrastructure.Data;
using Order.Domain.Interfaces;

namespace Order.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
	private readonly DatabaseContext _db;

	public UserRepository(DatabaseContext db)
	{
		_db = db;
	}

	public async Task<User> CreateAsync(User entity)
	{
		var entry = await _db.Users.AddAsync(entity);	

		return entry.Entity;
	}

	public void Delete(User entity)
	{
		_db.Users.Remove(entity);		
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

	public async Task<IEnumerable<User>> GetAllAsync()
	{
		return await _db.Users
			.Include(user => user.Orders)!
				.ThenInclude(order => order.Payloads)
			.AsNoTracking()
			.ToListAsync();		
	}

	public async Task<User?> GetByIdAsync(long id)
	{
		return await _db.Users
			.Include(user => user.Orders)!
				.ThenInclude(order => order.Payloads)
			.AsNoTracking()
			.FirstOrDefaultAsync(user => user.Id == id);
	}

	public User Update(User entity)
	{
		var entry = _db.Users.Update(entity);		

		return entry.Entity;
	}

	public async Task SaveChangesAsync()
	{
		await _db.SaveChangesAsync();
	}
}
