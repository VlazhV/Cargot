using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Order.Application.DTOs.UserDTOs;
using Order.Application.Interfaces;
using Order.WebApi.Extensions;

namespace Order.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[AuthorizeAdminManager]
public class UsersController: ControllerBase
{
	private readonly IUserService _userService;
	
	public UsersController(IUserService userService)
	{
		_userService = userService;
	}
	
	[HttpGet]
	public async Task<ActionResult<IEnumerable<GetUserDto>>> GetAllAsync()
	{
		return Ok(await _userService.GetAllAsync());
	}
	
	[HttpPost]
	public async Task<ActionResult<GetUserDto>> CreateAsync([FromBody] UpdateUserDto userDto)
	{
		return Ok(await _userService.CreateAsync(userDto));
	}
	
	[HttpGet("{id}")]
	public async Task<ActionResult<GetUserInfoDto>> GetByIdAsync(long id)
	{
		return Ok(await _userService.GetByIdAsync(id));
	}
	
	[HttpDelete("{id}")]
	public async Task<ActionResult> DeleteAsync(long id)
	{
		await _userService.DeleteAsync(id);
	
		return NoContent();
	}
	
	[HttpPut("{id}")]
	public async Task<ActionResult<GetUserDto>> UpdateAsync(long id, [FromBody] UpdateUserDto userDto)
	{
		return Ok(await _userService.UpdateAsync(id, userDto));
	}
	
}
