using Autopark.Business.DTOs.SheduleDtos;
using Autopark.Business.DTOs.TrailerDTOs;
using Autopark.Business.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Autopark.Presentation.Controllers;

[Route("api/trailers")]
[ApiController]
[Authorize(Roles = "admin, manager")]
public class TrailersController : ControllerBase
{
	private readonly ITrailerService _trailerService;
	private readonly ITrailerSheduleService _sheduleService;
	
	public TrailersController(ITrailerService trailerService, ITrailerSheduleService sheduleService)
	{
		_trailerService = trailerService;
		_sheduleService = sheduleService;
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
	public async Task<ActionResult<GetSheduleDto>> AddSheduleAsync
		([FromRoute] int trailerId, UpdatePlanSheduleDto sheduleDto)
	{
		return Ok(await _sheduleService.AddPlannedSheduleAsync(trailerId, sheduleDto));
	}
	
	[HttpGet("{trailerId}/shedules")]
	public async Task<ActionResult<IEnumerable<GetSheduleDto>>> GetShedulesOfTrailerAsync([FromRoute] int trailerId)
	{
		return Ok(await _sheduleService.GetShedulesOfVehicleAsync(trailerId));
	}	
}
