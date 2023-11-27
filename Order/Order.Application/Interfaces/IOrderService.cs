using System.Security.Claims;
using Order.Application.DTOs.OrderDTOs;
using Order.Application.DTOs.PayloadDTOs;

namespace Order.Application.Interfaces;

public interface IOrderService
{
	Task<IEnumerable<GetOrderInfoDto>> GetAllAsync();
	Task<GetOrderInfoDto> GetByIdAsync(long id, ClaimsPrincipal user);
	Task<GetOrderDto> CreateAsync(long? customerId, ClaimsPrincipal user, UpdateOrderPayloadsDto orderDto);
	Task<GetOrderDto> UpdateAsync(long id, ClaimsPrincipal user, UpdateOrderDto orderDto);
	Task<GetOrderInfoDto> UpdatePayloadListAsync(long id, ClaimsPrincipal user, IEnumerable<UpdatePayloadDto> payloadDtos);
	Task DeleteAsync(long id, ClaimsPrincipal user);
	Task<GetOrderDto> SetStatusAsync(long id, string status, ClaimsPrincipal user);
}
