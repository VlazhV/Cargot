using Microsoft.EntityFrameworkCore;
using Identity.DataAccess.Data;
using Identity.DataAccess.Interfaces;
using Identity.DataAccess.Entities;
using Microsoft.AspNetCore.Identity;
using Identity.DataAccess.Specifications;

namespace Identity.DataAccess.Repositories;

public class UserRepository: IUserRepository
{
	private readonly DatabaseContext _db;
	private readonly UserManager<IdentityUser<long>> _userManager;
	
	public UserRepository(DatabaseContext db, UserManager<IdentityUser<long>> userManager)
	{
		_db = db;
		_userManager = userManager;
	}

	public Task<IdentityResult> AddToRoleAsync(IdentityUser<long> user, string role)
	{
		return _userManager.AddToRoleAsync(user, role);
	}

	public Task<IdentityResult> ChangePasswordAsync(IdentityUser<long> user, string oldPassword, string newPassword)
	{
		return _userManager.ChangePasswordAsync(user, oldPassword, newPassword);
	}

	public Task<bool> CheckPasswordAsync(IdentityUser<long> user, string password)
	{
		return _userManager.CheckPasswordAsync(user, password);
	}

	public Task<IdentityResult> CreateAsync(IdentityUser<long> user, string password)
	{
		return _userManager.CreateAsync(user, password);
	}

	public Task<IdentityResult> DeleteAsync(IdentityUser<long> user)
	{
		return _userManager.DeleteAsync(user);
	}

	public bool DoesItExist(IdentityUser<long> candidate)
	{
		return _db.Users.Any(u => u.Email!.Equals(candidate.Email)
					|| u.UserName!.Equals(candidate.UserName)
					|| u.PhoneNumber!.Equals(candidate.PhoneNumber));
		
	}

	public Task<IdentityUser<long>?> FindByIdAsync(string id)
	{
		return _userManager.FindByIdAsync(id);
	}

	public Task<IdentityUser<long>?> FindByNameAsync(string name)
	{
		return _userManager.FindByNameAsync(name);
	}

	public async Task<IEnumerable<IdentityUser<long>>> GetAllWithSpec(UserSpecification specification)
	{
		IQueryable<IdentityUser<long>> query = _db.Users;

		query = specification.Build(query);

		return await query.ToListAsync();
	}

	public Task<IList<string>> GetRolesAsync(IdentityUser<long> user)
	{
		return _userManager.GetRolesAsync(user);
	}

	public Task<IdentityResult> UpdateAsync(IdentityUser<long> user)
	{
		return _userManager.UpdateAsync(user);
	}
}