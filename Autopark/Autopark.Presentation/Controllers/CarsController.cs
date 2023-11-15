using Autopark.Business.DTOs.CarDTOs;
using Autopark.Business.DTOs.SheduleDtos;
using Autopark.Business.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Autopark.Presentation.Controllers;

[Route("api/cars")]
[ApiController]
public class CarsController: ControllerBase
{
	private readonly ICarService _carService;
	
	public CarsController(ICarService carService)
	{
		_carService = carService;
	}
	
	
	[HttpGet("{id}")]
	public async Task<ActionResult<GetCarAutoparkDto>> GetByIdAsync([FromRoute] int id)
	{
		return Ok(await _carService.GetByIdAsync(id));
	}
	
	[HttpGet]
	public async Task<ActionResult<IEnumerable<GetCarAutoparkDto>>> GetAllAsync()
	{
		return Ok(await _carService.GetAllAsync());
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
		return Ok(await _carService.GetShedulesAsync(carId));
	}
	
	[HttpPost("{carId}/shedules")]
	public async Task<ActionResult<GetSheduleDto>> AddSheduleAsync([FromRoute] int carId, UpdatePlanSheduleDto sheduleDto)
	{
		return Ok(await _carService.AddPlannedSheduleAsync(carId, sheduleDto));
	}
	
	[HttpGet("{carId}/shedules/{id}")]
	public async Task<ActionResult<GetSheduleDto>> GetSheduleAsync([FromRoute] int id)
	{
		return Ok(await _carService.GetSheduleByIdAsync(id));
	}
	
	[HttpPut("{carId}/shedules/{id}")]
	public async Task<ActionResult<GetSheduleDto>> UpdateSheduleAsync([FromRoute] int id, UpdatePlanSheduleDto sheduleDto)
	{
		return Ok(await _carService.UpdatePlannedSheduleAsync(id, sheduleDto));
	}
	
	[HttpPost("{carId}/shedules/{id}")]
	public async Task<ActionResult<GetSheduleDto>> UpdateActualSheduleAsync([FromRoute] int id)
	{
		return Ok(await _carService.UpdateActualSheduleAsync(id));
	}
	
	[HttpDelete("{carId}/shedules/{id}")]
	public async Task<ActionResult> DeleteSheduleAsync([FromRoute] int id)
	{
		await _carService.DeleteSheduleAsync(id);
		return Ok();
	}
}
