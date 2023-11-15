using Autopark.Business.DTOs.AutoparkDTOs;
using Autopark.Business.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Autopark.Presentation.Controllers;

[Route("api/autoparks")]
[ApiController]
public class AutoparksController : ControllerBase
{
	private readonly IAutoparkService _autoparkService;
	
	public AutoparksController(IAutoparkService autoparkService)
	{
		_autoparkService = autoparkService;
	}

		
	[HttpGet("{id}")]
	public async Task<ActionResult<GetAutoparkVehicleDto>> GetByIdAsync([FromRoute] int id)
	{
		return Ok(await _autoparkService.GetByIdAsync(id));
	}
	
	[HttpGet]
	public async Task<ActionResult<IEnumerable<GetAutoparkVehicleDto>>> GetAllAsync()
	{
		return Ok(await _autoparkService.GetAllAsync());
	}	
	
	[HttpPost]
	public async Task<ActionResult<GetAutoparkVehicleDto>> CreateAsync([FromBody] UpdateAutoparkDto autoparkDto)
	{
		return Ok(await _autoparkService.CreateAsync(autoparkDto));
	}
	
	[HttpDelete("{id}")]
	public async Task<ActionResult> DeleteAsync([FromRoute] int id)
	{
		await _autoparkService.DeleteAsync(id);
		return Ok();
	}

	[HttpPut("{id}")]
	public async Task<ActionResult<GetAutoparkVehicleDto>> UpdateAsync([FromRoute] int id, UpdateAutoparkDto autoparkDto)
	{
		return Ok(await _autoparkService.UpdateAsync(id, autoparkDto));
	}	
}
