using Autopark.Business.DTOs.ScheduleDtos;
using Autopark.Business.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Autopark.Presentation.Controllers;

[Route("api/cars/schedules/{id}")]
[ApiController]
[Authorize(Roles = "manager, admin")]
public class CarSchedulesController : ControllerBase
{
	private readonly ICarScheduleService _scheduleService;
	
	public CarSchedulesController(ICarScheduleService scheduleService)
	{
		_scheduleService = scheduleService;
	}
	
	[HttpGet]
	public async Task<ActionResult<GetScheduleDto>> GetScheduleAsync([FromRoute] int id)
	{
		return Ok(await _scheduleService.GetScheduleByIdAsync(id));
	}
	
	[HttpPut]
	public async Task<ActionResult<GetScheduleDto>> UpdateScheduleAsync([FromRoute] int id, UpdatePlanScheduleDto scheduleDto)
	{
		return Ok(await _scheduleService.UpdatePlannedScheduleAsync(id, scheduleDto));
	}
	
	[HttpPost]
	public async Task<ActionResult<GetScheduleDto>> UpdateActualScheduleAsync([FromRoute] int id)
	{
		return Ok(await _scheduleService.UpdateActualScheduleAsync(id));
	}
	
	[HttpDelete]
	public async Task<ActionResult> DeleteScheduleAsync([FromRoute] int id)
	{
		await _scheduleService.DeleteScheduleAsync(id);
		
		return NoContent();
	}
}
