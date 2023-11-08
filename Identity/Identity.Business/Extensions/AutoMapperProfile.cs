using AutoMapper;
using Identity.Business.DTOs;
using Microsoft.AspNetCore.Identity;

namespace Identity.Business.Extensions;

public class AutoMapperProfile: Profile
{
	public AutoMapperProfile()
	{
		CreateMap<IdentityUser<long>, UserDto>();
		CreateMap<UserDto, IdentityUser<long>>();
		CreateMap<SignupDto, IdentityUser<long>>();
		CreateMap<RegisterDto, IdentityUser<long>>();
		CreateMap<IdentityUser<long>, UserIdDto>();
	}
}
