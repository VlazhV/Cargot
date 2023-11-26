using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Order.Application.DTOs.OrderDTOs;
using Order.Application.DTOs.PayloadDTOs;
using Order.Application.Interfaces;

namespace Order.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class OrdersController: ControllerBase
{
	private readonly IOrderService _orderService;
	
	public OrdersController(IOrderService orderService)
	{
		_orderService = orderService;
	}
	
	[HttpGet]
	public async Task<ActionResult<IEnumerable<GetOrderInfoDto>>> GetAllAsync()
	{
		return Ok(await _orderService.GetAllAsync());
	}
	
	[HttpGet("{id}")]
	public async Task<ActionResult<GetOrderInfoDto>> GetByIdAsync(long id)
	{
		return Ok(await _orderService.GetByIdAsync(id));
	}
	
	[HttpPost]
	public async Task<ActionResult<GetOrderDto>> CreateAsync([FromBody] UpdateOrderPayloadsDto orderDto)
	{
		return Ok(await _orderService.CreateAsync(orderDto));
	}
	
	[HttpPut("{id}")]
	public async Task<ActionResult<GetOrderDto>> UpdateAsync(long id, [FromBody] UpdateOrderDto orderDto)
	{
		return Ok(await _orderService.UpdateAsync(id, orderDto));
	}
	
	[HttpPatch("{id}")]
	public async Task<ActionResult<GetOrderInfoDto>> UpdatePayloadListAsync
		(long id, [FromBody] IEnumerable<UpdatePayloadDto> payloadDtos)
	{
		return Ok(await _orderService.UpdatePayloadListAsync(id, payloadDtos));
	}
	
	[HttpDelete("{id}")]
	public async Task<ActionResult> DeleteAsync(long id)
	{
		await _orderService.DeleteAsync(id);

		return NoContent();
	}
}
