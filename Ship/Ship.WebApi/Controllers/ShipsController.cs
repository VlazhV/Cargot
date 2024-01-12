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
	public async Task<ActionResult<IEnumerable<GetShipDto>>> GetAllAsync([FromQuery] PagingDto pagingDto)
	{
		return Ok(await _shipService.GetAllAsync(pagingDto));
	}
	
	[HttpGet("{id}")]
	[AuthorizeAdminManagerDriver]
	public async Task<ActionResult<GetShipDto>> GetByIdAsync([FromRoute] string id)
	{
		return Ok(await _shipService.GetByIdAsync(id));
	}
	
	[HttpDelete("{id}")]
	[AuthorizeAdminManager]
	public async Task<ActionResult> DeleteAsync([FromRoute] string id)
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
	public async Task<ActionResult<GetShipDto>> UpdateAsync([FromRoute] string id, [FromBody] UpdateShipDto shipDto)
	{
		return Ok(await _shipService.UpdateAsync(id, shipDto));
	}
	
	[HttpPatch("{id}")]
	[AuthorizeAdminManagerDriver]
	public async Task<ActionResult<GetShipDto>> MarkAsync([FromRoute] string id)
	{
		return Ok(await _shipService.MarkAsync(id));
	}
}
