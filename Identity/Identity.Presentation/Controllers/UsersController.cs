using System.Security.Claims;
using Identity.Business.DTOs;
using Identity.Business.Interfaces;
using Identity.DataAccess.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Presentation.Controllers;



[ApiController]
[Route("api/users")]
public class UsersController : ControllerBase
{
	private IUserService _userSerivce;
	public UsersController
		(IUserService userService)
	{
		_userSerivce = userService;
	}
	
	[HttpGet]
	[Authorize]
	public async Task<ActionResult<IEnumerable<UserIdDto>>> GetAll([FromQuery] string? userName, [FromQuery] string? phone, [FromQuery] string? email)
	{
		var role = User.FindFirst(ClaimTypes.Role)?.Value;
		if (role != Role.Admin && role != Role.Manager)
			return NotFound();
		return Ok(await _userSerivce.GetSpecified(userName, phone, email));
	}
	
	[HttpGet("{id}")]
	[Authorize]
	public async Task<ActionResult<UserDto>> Get(string id) 
	{
		var role = User.FindFirst(ClaimTypes.Role)?.Value;
		if (role != Role.Admin && role != Role.Manager)
			return NotFound();
		return Ok(await _userSerivce.GetById(id));
	}
	
	[HttpGet("profile")]
	[Authorize]
	public async Task<ActionResult<UserDto>> GetProfile()
	{
		return Ok(await _userSerivce.GetById(User.FindFirst(ClaimTypes.NameIdentifier)?.Value));
	}
	
	[HttpPut("profile")]
	[Authorize]
	public async Task<ActionResult<UserDto>> UpdateProfile([FromBody] UserUpdateDto userDto)	
	{
		return Ok(await _userSerivce.Update(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, userDto));
	}
	
	[HttpPut("profile/password")]
	[Authorize]
	public async Task<ActionResult> UpdatePassword([FromBody] PasswordDto passwordDto)
	{
		await _userSerivce.UpdatePassword(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, passwordDto);
		return Ok();
	}
		
}