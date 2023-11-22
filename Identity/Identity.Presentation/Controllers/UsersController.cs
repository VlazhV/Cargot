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
	public async Task<ActionResult<IEnumerable<UserIdDto>>> GetAllAsync([FromQuery] SpecDto specDto)
	{		
		return Ok(await _userSerivce.GetAllWithSpecAsync(specDto, User.FindFirst(ClaimTypes.Role)?.Value));
	}
	
	[HttpGet("{id}")]
	[Authorize]
	public async Task<ActionResult<UserDto>> GetByIdAsync(string id) 
	{		
		return Ok(await _userSerivce.GetByIdAsync(id, User.FindFirst(ClaimTypes.Role)?.Value));
	}
	
	[HttpGet("profile")]
	[Authorize]
	public async Task<ActionResult<UserDto>> GetProfileAsync()
	{
		return Ok(await _userSerivce.GetByIdAsync(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, null));
	}
	
	[HttpPut("profile")]
	[Authorize]
	public async Task<ActionResult<UserDto>> UpdateProfileAsync([FromBody] UserUpdateDto userDto)	
	{
		return Ok(await _userSerivce.UpdateAsync(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, userDto));
	}
	
	[HttpPut("profile/password")]
	[Authorize]
	public async Task<ActionResult> UpdatePasswordAsync([FromBody] PasswordDto passwordDto)
	{
		await _userSerivce.UpdatePasswordAsync(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, passwordDto);
		return Ok();
	}
	
	[HttpDelete("{id}")]
	[Authorize]
	public async Task<ActionResult> DeleteUserAsync ([FromRoute] string? id)
	{
		await _userSerivce.DeleteAsync(id, User.FindFirst(ClaimTypes.Role)?.Value);
		return Ok();
	}
		
}