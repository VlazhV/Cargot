using Autopark.Business.DTOs;
using Autopark.Business.DTOs.CarDTOs;
using Autopark.Business.DTOs.ScheduleDtos;
using Autopark.Business.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Autopark.Presentation.Controllers;

[Route("api/cars")]
[ApiController]
[Authorize(Roles = "admin, manager")]
public class CarsController: ControllerBase
{
	private readonly ICarService _carService;
	private readonly ICarScheduleService _scheduleService;
	
	public CarsController(ICarService carService, ICarScheduleService scheduleService)
	{
		_carService = carService;
		_scheduleService = scheduleService;
	}
	
	
	[HttpGet("{id}")]
	public async Task<ActionResult<GetCarAutoparkDto>> GetByIdAsync([FromRoute] int id)
	{
		return Ok(await _carService.GetByIdAsync(id));
	}
	
	[HttpGet]
	public async Task<ActionResult<IEnumerable<GetCarAutoparkDto>>> GetWithSpecsAsync([FromQuery] SpecDto specDto)
	{
		return Ok(await _carService.GetWithSpecsAsync(specDto));
	}
	
	[HttpPost]
	public async Task<ActionResult<GetCarDto>> CreateAsync([FromBody] UpdateCarDto carDto)
	{
		return Ok(await _carService.CreateAsync(carDto));
	}
	
	[HttpDelete("{id}")]
	public async Task<ActionResult> DeleteAsync([FromRoute] int id)
	{
		await _carService.DeleteAsync(id);
		
		return NoContent();
	}
	
	[HttpPut("{id}")]
	public async Task<ActionResult<GetCarDto>> UpdateAsync([FromRoute] int id, UpdateCarDto carDto)
	{
		return Ok(await _carService.UpdateAsync(id, carDto));	
	}
	
	[HttpGet("{carId}/schedules")]
	public async Task<ActionResult<IEnumerable<GetScheduleDto>>> GetSchedulesOfCarAsync([FromRoute] int carId)
	{
		return Ok(await _scheduleService.GetSchedulesOfVehicleAsync(carId));
	}
	
	[HttpPost("{carId}/schedules")]
	public async Task<ActionResult<GetScheduleDto>> AddScheduleAsync([FromRoute] int carId, UpdatePlanScheduleDto scheduleDto)
	{
		return Ok(await _scheduleService.AddPlannedScheduleAsync(carId, scheduleDto));
	}
}
