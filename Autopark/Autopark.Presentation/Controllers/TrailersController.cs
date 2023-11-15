using Autopark.Business.DTOs.SheduleDtos;
using Autopark.Business.DTOs.TrailerDTOs;
using Autopark.Business.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Autopark.Presentation.Controllers;

[Route("api/trailers")]
[ApiController]
public class TrailersController : ControllerBase
{
	private readonly ITrailerService _trailerService;
	
	public TrailersController(ITrailerService trailerService)
	{
		_trailerService = trailerService;
	}

		
	[HttpGet("{id}")]
	public async Task<ActionResult<GetTrailerAutoparkDto>> GetByIdAsync([FromRoute] int id)
	{
		return Ok(await _trailerService.GetByIdAsync(id));
	}
	
	[HttpGet]
	public async Task<ActionResult<IEnumerable<GetTrailerAutoparkDto>>> GetAllAsync()
	{
		return Ok(await _trailerService.GetAllAsync());
	}	
	
	[HttpPost]
	public async Task<ActionResult<GetTrailerDto>> CreateAsync([FromBody] UpdateTrailerDto trailerDto)
	{
		return Ok(await _trailerService.CreateAsync(trailerDto));
	}
	
	[HttpDelete("{id}")]
	public async Task<ActionResult> DeleteAsync([FromRoute] int id)
	{
		await _trailerService.DeleteAsync(id);
		return Ok();
	}

	[HttpPut("{id}")]
	public async Task<ActionResult<GetTrailerDto>> UpdateAsync([FromRoute] int id, UpdateTrailerDto trailerDto)
	{
		return Ok(await _trailerService.UpdateAsync(id, trailerDto));
	}	
	
	[HttpPost("{trailerId}/shedules")]
	public async Task<ActionResult<GetSheduleDto>> AddSheduleAsync([FromRoute] int trailerId, UpdatePlanSheduleDto sheduleDto)
	{
		return Ok(await _trailerService.AddPlannedSheduleAsync(trailerId, sheduleDto));
	}
	
	[HttpPut("{trailerId}/shedules/{id}")]
	public async Task<ActionResult<GetSheduleDto>> UpdateSheduleAsync([FromRoute] int id, UpdatePlanSheduleDto sheduleDto)
	{
		return Ok(await _trailerService.UpdatePlannedSheduleAsync(id, sheduleDto));
	}
	
	[HttpPost("{trailerId}/shedules/{id}")]
	public async Task<ActionResult<GetSheduleDto>> UpdateActualSheduleAsync([FromRoute] int id)
	{
		return Ok(await _trailerService.UpdateActualSheduleAsync(id));
	}
	
	[HttpDelete("{trailerId}/shedules/{id}")]
	public async Task<ActionResult> DeleteSheduleAsync([FromRoute] int id)
	{
		await _trailerService.DeleteSheduleAsync(id);
		return Ok();
	}
}
