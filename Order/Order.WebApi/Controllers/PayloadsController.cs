using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Order.Application.DTOs.PayloadDTOs;
using Order.Application.Interfaces;
using Order.WebApi.Extensions;

namespace Order.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class PayloadsController: ControllerBase
{
	private readonly IPayloadService _payloadService;
	
	public PayloadsController(IPayloadService payloadService)
	{
		_payloadService = payloadService;
	}
	
	[HttpGet]
	[AuthorizeAdminManager]
	public async Task<ActionResult<IEnumerable<GetPayloadOrderDto>>> GetAllAsync()
	{
		return Ok(await _payloadService.GetAllAsync());
	}
	
	[HttpGet("{id}")]
	public async Task<ActionResult<GetPayloadOrderDto>> GetByIdAsync(long id)
	{
		return Ok(await _payloadService.GetByIdAsync(id, User));		
	}
	
	[HttpPut("{id}")]
	public async Task<ActionResult<GetPayloadDto>> UpdateAsync(long id, [FromBody] UpdatePayloadDto payloadDto)
	{
		return Ok(await _payloadService.UpdateAsync(id, User, payloadDto));
	}
	
	[HttpDelete("{id}")]
	public async Task<ActionResult> DeleteAsync(long id)
	{
		await _payloadService.DeleteAsync(id, User);

		return NoContent();
	}
}
