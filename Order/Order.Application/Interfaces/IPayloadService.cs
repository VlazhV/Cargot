using Order.Application.DTOs.PayloadDTOs;

namespace Order.Application.Interfaces;

public interface IPayloadService
{
	Task<GetPayloadOrderDto> GetByIdAsync(long id);
	Task<IEnumerable<GetPayloadOrderDto>> GetAllAsync();
	Task<GetPayloadDto> CreateAsync(UpdatePayloadDto payloadDto);
	Task<GetPayloadDto> UpdateAsync(long id, UpdatePayloadDto payloadDto);
	Task DeleteAsync(long id);
}
