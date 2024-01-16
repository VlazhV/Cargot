using Autopark.gRPC.Services;
using Microsoft.AspNetCore.Mvc;
using Ship.Application.DTOs;
using Autopark.gRPC.Requests;
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
	//[AuthorizeAdminManagerDriver]
	public async Task<ActionResult<IEnumerable<GetShipDto>>> GetAllAsync([FromQuery] PagingDto pagingDto, CancellationToken cancellationToken)
	{
		return Ok(await _shipService.GetAllAsync(pagingDto, cancellationToken));
	}
	
	[HttpGet("{id}")]
	[AuthorizeAdminManagerDriver]
	public async Task<ActionResult<GetShipDto>> GetByIdAsync([FromRoute] string id, CancellationToken cancellationToken)
	{
		return Ok(await _shipService.GetByIdAsync(id, cancellationToken));
	}
	
	[HttpDelete("{id}")]
	[AuthorizeAdminManager]
	public async Task<ActionResult> DeleteAsync([FromRoute] string id, CancellationToken cancellationToken)
	{
		await _shipService.DeleteAsync(id, cancellationToken);
		
		return NoContent();
	}
	
	[HttpPost]
//	[AuthorizeAdminManager]
	public async Task<ActionResult<GetShipDto>> CreateAsync([FromBody] UpdateShipDto shipDto, CancellationToken cancellationToken)
	{
		return Ok(await _shipService.CreateAsync(shipDto, cancellationToken));
	}
	
	[HttpPost("auto")]
	[AuthorizeAdminManager]
	public async Task<ActionResult<GetShipDto>> GenerateAsync([FromBody] GenerateShipDto shipDto, CancellationToken cancellationToken)
	{
		return Ok(await _shipService.GenerateShipAsync(shipDto, cancellationToken));
	}
	
	[HttpPut("{id}")]
	[AuthorizeAdminManager]
	public async Task<ActionResult<GetShipDto>> UpdateAsync([FromRoute] string id, [FromBody] UpdateShipDto shipDto, CancellationToken cancellationToken)
	{
		return Ok(await _shipService.UpdateAsync(id, shipDto, cancellationToken));
	}
	
	[HttpPatch("{id}")]
	[AuthorizeAdminManagerDriver]
	public async Task<ActionResult<GetShipDto>> MarkAsync([FromRoute] string id, CancellationToken cancellationToken)
	{
		return Ok(await _shipService.MarkAsync(id, cancellationToken));
	}
}
