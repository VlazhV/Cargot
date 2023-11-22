using System.Linq.Expressions;
using Microsoft.AspNetCore.Identity;

namespace Identity.DataAccess.Specifications;

public class UserSpecification
{
	private string? _phoneNumber;
	private string? _email;
	private string? _userName;
	
	private List<Expression<Func<IdentityUser<long>, bool>>?> _filters = null!;
	
	public UserSpecification(string? phoneNumber = null, string? email = null, string? username = null)
	{
		_email = email;
		_phoneNumber = phoneNumber;
		_userName = username;
		GenerateFilters();
	}

	private void GenerateFilters()
	{
		Expression<Func<IdentityUser<long>, bool>>? emailExp = null;
		if (_email is not null)
			emailExp = u => u.Email!.Contains(_email);
			
		Expression<Func<IdentityUser<long>, bool>>? phoneExp = null;
		if (_phoneNumber is not null)
			phoneExp = u => u.PhoneNumber!.Contains(_phoneNumber);
		
		Expression<Func<IdentityUser<long>, bool>>? nameExp = null;
		if (_userName is not null)
			nameExp = u => u.UserName!.Contains(_userName);

		_filters = new List<Expression<Func<IdentityUser<long>, bool>>?>()
		{
			emailExp,
			phoneExp,
			nameExp,
		};
	}
	
	

	public IQueryable<IdentityUser<long>> Build(IQueryable<IdentityUser<long>> sourceQuery)
	{
		
		foreach (var f in _filters)
		{
			if (f is not null)
				sourceQuery = sourceQuery.Where(f);
		}
		return sourceQuery;
	}
	
}