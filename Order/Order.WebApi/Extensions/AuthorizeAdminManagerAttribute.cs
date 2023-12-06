using Microsoft.AspNetCore.Authorization;
using Order.Domain.Constants;

namespace Order.WebApi.Extensions;

public class AuthorizeAdminManagerAttribute: AuthorizeAttribute
{
	public AuthorizeAdminManagerAttribute(): base()
	{
		Roles = string.Concat(Domain.Constants.Roles.Admin, ", ", Domain.Constants.Roles.Manager);
	}
	
	public AuthorizeAdminManagerAttribute(string policy): base(policy)
	{
		Roles = string.Concat(Domain.Constants.Roles.Admin, ", ", Domain.Constants.Roles.Manager);
	}
}