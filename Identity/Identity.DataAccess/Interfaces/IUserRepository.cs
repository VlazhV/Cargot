using Microsoft.AspNetCore.Identity;
using Identity.DataAccess.Specifications;

namespace Identity.DataAccess.Interfaces;

public interface IUserRepository
{
	Task<IdentityUserToken<long>?> GetTokenAsync(IdentityUser<long> user);
	Task UpdateTokenAsync(IdentityUserToken<long> token, string? value);
	Task<IEnumerable<IdentityUser<long>>> GetAllWithSpec(UserSpecification specification);
	bool DoesItExist(IdentityUser<long> candidate);
}