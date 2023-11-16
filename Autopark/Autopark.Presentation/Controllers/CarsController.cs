using Autopark.Business.DTOs;
using Autopark.Business.DTOs.CarDTOs;
using Autopark.Business.DTOs.SheduleDtos;
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
	private readonly ICarSheduleService _sheduleService;
	
	public CarsController(ICarService carService, ICarSheduleService sheduleService)
	{
		_carService = carService;
		_sheduleService = sheduleService;
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
		return Ok();
	}
	
	[HttpPut("{id}")]
	public async Task<ActionResult<GetCarDto>> UpdateAsync([FromRoute] int id, UpdateCarDto carDto)
	{
		return Ok(await _carService.UpdateAsync(id, carDto));	
	}
	
	[HttpGet("{carId}/shedules")]
	public async Task<ActionResult<IEnumerable<GetSheduleDto>>> GetShedulesOfCarAsync([FromRoute] int carId)
	{
		return Ok(await _sheduleService.GetShedulesOfVehicleAsync(carId));
	}
	
	[HttpPost("{carId}/shedules")]
	public async Task<ActionResult<GetSheduleDto>> AddSheduleAsync([FromRoute] int carId, UpdatePlanSheduleDto sheduleDto)
	{
		return Ok(await _sheduleService.AddPlannedSheduleAsync(carId, sheduleDto));
	}
}
