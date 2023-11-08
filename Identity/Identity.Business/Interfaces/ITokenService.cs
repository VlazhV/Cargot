using Microsoft.AspNetCore.Identity;
namespace Identity.Business.Interfaces;

public interface ITokenService
{
	Task<string> GetTokenAsync(IdentityUser<long> user);	
}