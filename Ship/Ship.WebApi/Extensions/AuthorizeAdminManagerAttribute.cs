using Microsoft.AspNetCore.Authorization;

namespace Ship.WebApi.Extensions;

public class AuthorizeAdminManagerAttribute: AuthorizeAttribute
{
	public AuthorizeAdminManagerAttribute(): base()
	{
		Roles = string.Concat(Application.Constants.Roles.Admin, ", ", Application.Constants.Roles.Manager);
	}
	
	public AuthorizeAdminManagerAttribute(string policy): base(policy)
	{
		Roles = string.Concat(Application.Constants.Roles.Admin, ", ", Application.Constants.Roles.Manager);
	}
}
