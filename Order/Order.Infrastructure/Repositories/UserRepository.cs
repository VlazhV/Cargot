using Microsoft.EntityFrameworkCore;
using Order.Domain.Entities;
using Order.Infrastructure.Data;
using Order.Domain.Interfaces;

namespace Order.Infrastructure.Repositories;

public class UserRepository: IUserRepository
{
	private readonly DatabaseContext _db;
	
	public UserRepository(DatabaseContext db)
	{
		_db = db;
	}

	public async Task<User> CreateAsync(User entity)
	{
		var entry = await _db.Users.AddAsync(entity);
		await _db.SaveChangesAsync();

		return entry.Entity;
	}

	public async Task DeleteAsync(User entity)
	{
		_db.Users.Remove(entity);
		await _db.SaveChangesAsync();				
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

	public async Task<User> UpdateAsync(User entity)
	{
		var entry = _db.Users.Update(entity);
		await _db.SaveChangesAsync();

		return entry.Entity;
	}

}
