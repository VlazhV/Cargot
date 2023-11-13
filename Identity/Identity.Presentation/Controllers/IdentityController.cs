using System.Reflection.Metadata;

using Microsoft.AspNetCore.Mvc;
using Identity.Business.Interfaces;
using Identity.Business.DTOs;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Identity.DataAccess.Entities;

namespace Identity.Presentation.Controllers;

[ApiController]
[Route("api")]
public class IdentityController : ControllerBase
{
	private IUserService _userService;
	public IdentityController(IUserService userService)
	{
		_userService = userService;
	}


	[HttpPost("login")]
	public async Task<ActionResult<TokenDto>> Login([FromBody] LoginDto loginModel)
	{
		return Ok(await _userService.LoginAsync(loginModel));
	}

	[HttpPost("sign-up")]
	public async Task<ActionResult<TokenDto>> SignUp([FromBody] SignupDto signupModel)
	{
		return Ok(await _userService.SignUpAsync(signupModel));
	}

	[HttpPost("register")]
	[Authorize]
	public async Task<ActionResult<UserDto>> Register([FromBody] RegisterDto registerDto)
	{				
		return Ok(await _userService.RegisterAsync(registerDto, User.FindFirst(ClaimTypes.Role)?.Value));		
	}
	
	
}
