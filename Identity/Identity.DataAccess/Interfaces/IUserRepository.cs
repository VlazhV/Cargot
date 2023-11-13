using Microsoft.AspNetCore.Identity;
using Identity.DataAccess.Specifications;

namespace Identity.DataAccess.Interfaces;

public interface IUserRepository
{
	Task<IEnumerable<IdentityUser<long>>> GetAllWithSpec(UserSpecification specification);
	bool DoesItExist(IdentityUser<long> candidate);
}