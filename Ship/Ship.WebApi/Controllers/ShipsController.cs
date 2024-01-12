using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ship.Application.DTOs;
using Ship.Application.Interfaces;
using Ship.WebApi.Extensions;

namespace Ship.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ShipsController: ControllerBase
{
	private readonly IShipService _shipService;
	
	public ShipsController(IShipService shipService)
	{
		_shipService = shipService;
	}
	
	[HttpGet]
	[AuthorizeAdminManagerDriver]
	public async Task<ActionResult<IEnumerable<GetShipDto>>> GetAllAsync()
	{
		return Ok(await _shipService.GetAllAsync());
	}
	
	[HttpGet("{id}")]
	[AuthorizeAdminManagerDriver]
	public async Task<ActionResult<GetShipDto>> GetByIdAsync(string id)
	{
		return Ok(await _shipService.GetByIdAsync(id));
	}
	
	[HttpDelete("{id}")]
	[AuthorizeAdminManager]
	public async Task<ActionResult> DeleteAsync(string id)
	{
		await _shipService.DeleteAsync(id);
		return NoContent();
	}
	
	[HttpPost]
	[AuthorizeAdminManager]
	public async Task<ActionResult<GetShipDto>> CreateAsync([FromBody] UpdateShipDto shipDto)
	{
		return Ok(await _shipService.CreateAsync(shipDto));
	}
	
	[HttpPut("{id}")]
	[AuthorizeAdminManager]
	public async Task<ActionResult<GetShipDto>> UpdateAsync(string id, [FromBody] UpdateShipDto shipDto)
	{
		return Ok(await _shipService.UpdateAsync(id, shipDto));
	}
	
	[HttpPatch("{id}")]
	[AuthorizeAdminManagerDriver]
	public async Task<ActionResult<GetShipDto>> MarkAsync(string id)
	{
		return Ok(await _shipService.MarkAsync(id));
	}
}
