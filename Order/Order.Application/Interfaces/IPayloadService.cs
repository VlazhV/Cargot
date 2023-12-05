using System.Security.Claims;
using Order.Application.DTOs.PayloadDTOs;

namespace Order.Application.Interfaces;

public interface IPayloadService
{
	Task<GetPayloadOrderDto> GetByIdAsync(long id, ClaimsPrincipal user);
	Task<IEnumerable<GetPayloadOrderDto>> GetAllAsync();	
	Task<GetPayloadDto> UpdateAsync(long id, ClaimsPrincipal user, UpdatePayloadDto payloadDto);
	Task DeleteAsync(long id, ClaimsPrincipal user);
}
