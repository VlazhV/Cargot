using Autopark.Business.DTOs.SheduleDtos;
using Autopark.Business.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Autopark.Presentation.Controllers;

[Route("api/cars/shedules/{id}")]
[ApiController]
[Authorize(Roles = "manager, admin")]
public class CarShedulesController : ControllerBase
{
	private readonly ICarSheduleService _sheduleService;
	
	public CarShedulesController(ICarSheduleService sheduleService)
	{
		_sheduleService = sheduleService;
	}
	
	[HttpGet]
	public async Task<ActionResult<GetSheduleDto>> GetSheduleAsync([FromRoute] int id)
	{
		return Ok(await _sheduleService.GetSheduleByIdAsync(id));
	}
	
	[HttpPut]
	public async Task<ActionResult<GetSheduleDto>> UpdateSheduleAsync([FromRoute] int id, UpdatePlanSheduleDto sheduleDto)
	{
		return Ok(await _sheduleService.UpdatePlannedSheduleAsync(id, sheduleDto));
	}
	
	[HttpPost]
	public async Task<ActionResult<GetSheduleDto>> UpdateActualSheduleAsync([FromRoute] int id)
	{
		return Ok(await _sheduleService.UpdateActualSheduleAsync(id));
	}
	
	[HttpDelete]
	public async Task<ActionResult> DeleteSheduleAsync([FromRoute] int id)
	{
		await _sheduleService.DeleteSheduleAsync(id);
		return Ok();
	}
}
