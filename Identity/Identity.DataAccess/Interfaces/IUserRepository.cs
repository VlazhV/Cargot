using Microsoft.AspNetCore.Identity;
using Identity.DataAccess.Specifications;

namespace Identity.DataAccess.Interfaces;

public interface IUserRepository
{
	Task<IEnumerable<IdentityUser<long>>> GetAllWithSpec(UserSpecification specification);
	bool DoesItExist(IdentityUser<long> candidate);
	
	Task<IList<string>> GetRolesAsync(IdentityUser<long> user);
	Task<IdentityUser<long>?> FindByNameAsync(string name);
	Task<bool> CheckPasswordAsync(IdentityUser<long> user, string password);
	Task<IdentityResult> CreateAsync(IdentityUser<long> user, string password);
	Task<IdentityResult> UpdateAsync(IdentityUser<long> user);
	Task<IdentityResult> AddToRoleAsync(IdentityUser<long> user, string role);
	Task<IdentityUser<long>?> FindByIdAsync(string id);
	Task<IdentityResult> ChangePasswordAsync(IdentityUser<long> user, string oldPassword, string newPassword);
	Task<IdentityResult> DeleteAsync(IdentityUser<long> user);
	
}