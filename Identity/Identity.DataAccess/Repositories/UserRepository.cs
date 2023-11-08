using Microsoft.EntityFrameworkCore;
using Identity.DataAccess.Data;
using Identity.DataAccess.Interfaces;
using Identity.DataAccess.Entities;
using Microsoft.AspNetCore.Identity;
using Identity.DataAccess.Specifications;

namespace Identity.DataAccess.Repositories;

public class UserRepository: IUserRepository
{
	private DatabaseContext _db;
	
	public UserRepository(DatabaseContext db)
	{
		_db = db;
	}

	public async Task<IEnumerable<IdentityUser<long>>> GetSpecified(UserSpecification specification)
	{
		IQueryable<IdentityUser<long>> query = _db.Users;

		query = specification.Build(query);

		return await query.ToListAsync();
	}
	
		

	public async Task<IdentityUserToken<long>?> GetTokenAsync(IdentityUser<long> user)
	{
		var ut = await _db.UserTokens.FirstOrDefaultAsync(ut => ut.UserId == user.Id);
		return ut;
	}

	public async Task UpdateTokenAsync(IdentityUserToken<long> token, string? value)
	{
		token.Value = value;
		_db.UserTokens.Update(token);
		await _db.SaveChangesAsync();
	}		
}