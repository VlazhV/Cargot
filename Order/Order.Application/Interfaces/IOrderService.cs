using Order.Application.DTOs.OrderDTOs;
using Order.Application.DTOs.PayloadDTOs;

namespace Order.Application.Interfaces;

public interface IOrderService
{
	Task<IEnumerable<GetOrderInfoDto>> GetAllAsync();
	Task<GetOrderInfoDto> GetByIdAsync(long id);
	Task<GetOrderDto> CreateAsync(UpdateOrderDto orderDto);
	Task<GetOrderDto> UpdateAsync(long id, UpdateOrderDto orderDto);
	Task<GetOrderInfoDto> UpdatePayloadListAsync(long id, IEnumerable<UpdatePayloadDto> payloadDtos);
	Task DeleteAsync(long id);
	Task<GetOrderDto> SetStatusAsync(long id, string status);
}
