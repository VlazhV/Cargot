using Microsoft.AspNetCore.Authorization;

namespace Ship.WebApi.Extensions;

public class AuthorizeAdminManagerDriverAttribute: AuthorizeAttribute
{
	public AuthorizeAdminManagerDriverAttribute(): base()
	{
		Roles = string.Concat(Application.Constants.Roles.Admin, ", ", 
			Application.Constants.Roles.Manager, ", ",
			Application.Constants.Roles.Driver);
	}
	
	public AuthorizeAdminManagerDriverAttribute(string policy): base(policy)
	{
		Roles = string.Concat(Application.Constants.Roles.Admin, ", ", 
			Application.Constants.Roles.Manager, ", ",
			Application.Constants.Roles.Driver);
	}
}
