using Autopark.Business.DTOs;
using Autopark.Business.DTOs.ScheduleDtos;
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
	private readonly ITrailerScheduleService _scheduleService;
	
	public TrailersController(ITrailerService trailerService, ITrailerScheduleService scheduleService)
	{
		_trailerService = trailerService;
		_scheduleService = scheduleService;
	}

		
	[HttpGet("{id}")]
	public async Task<ActionResult<GetTrailerAutoparkDto>> GetByIdAsync([FromRoute] int id)
	{
		return Ok(await _trailerService.GetByIdAsync(id));
	}
	
	[HttpGet]
	public async Task<ActionResult<IEnumerable<GetTrailerAutoparkDto>>> GetWithSpecsAsync(SpecDto specDto)
	{
		return Ok(await _trailerService.GetWithSpecsAsync(specDto));
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
		
		return NoContent();
	}

	[HttpPut("{id}")]
	public async Task<ActionResult<GetTrailerDto>> UpdateAsync([FromRoute] int id, UpdateTrailerDto trailerDto)
	{
		return Ok(await _trailerService.UpdateAsync(id, trailerDto));
	}	
	
	[HttpPost("{trailerId}/schedules")]
	public async Task<ActionResult<GetScheduleDto>> AddScheduleAsync
		([FromRoute] int trailerId, UpdatePlanScheduleDto scheduleDto)
	{
		return Ok(await _scheduleService.AddPlannedScheduleAsync(trailerId, scheduleDto));
	}
	
	[HttpGet("{trailerId}/schedules")]
	public async Task<ActionResult<IEnumerable<GetScheduleDto>>> GetSchedulesOfTrailerAsync([FromRoute] int trailerId)
	{
		return Ok(await _scheduleService.GetSchedulesOfVehicleAsync(trailerId));
	}	
}
