using System.Security.Claims;
using AutoMapper;
using Order.Application.DTOs.PayloadDTOs;
using Order.Application.Exceptions;
using Order.Application.Interfaces;
using Order.Domain.Constants;
using Order.Domain.Entities;
using Order.Domain.Interfaces;

namespace Order.Application.Services;

public class PayloadService: IPayloadService
{
	private readonly IPayloadRepository _payloadRepository;
	private readonly IMapper _mapper;
	
	public PayloadService(IPayloadRepository payloadRepository, IMapper mapper)
	{
		_payloadRepository = payloadRepository;
		_mapper = mapper;
	}

	public async Task<GetPayloadDto> CreateAsync(UpdatePayloadDto payloadDto)
	{
		var payload = _mapper.Map<Payload>(payloadDto);

		payload = await _payloadRepository.CreateAsync(payload);

		return _mapper.Map<GetPayloadDto>(payload);
	}

	public async Task DeleteAsync(long id, ClaimsPrincipal user)
	{
		var role = user.FindFirst(ClaimTypes.Role)!.Value;
		var userId = long.Parse(user.FindFirst(ClaimTypes.NameIdentifier)!.Value);

		var payload = await _payloadRepository.GetByIdAsync(id)
			?? throw new ApiException("Payload is not found", ApiException.NotFound);

		if (!(role == Roles.Admin || role == Roles.Manager || userId == payload.Order.ClientId))
		{
			throw new ApiException("No permission", ApiException.Forbidden);
		}

		await _payloadRepository.DeleteAsync(payload);
	}

	public async Task<IEnumerable<GetPayloadOrderDto>> GetAllAsync()
	{
		var payloads = await _payloadRepository.GetAllAsync();

		return payloads.Select(payload => _mapper.Map<GetPayloadOrderDto>(payload));
	}

	public async Task<GetPayloadOrderDto> GetByIdAsync(long id, ClaimsPrincipal user)
	{
		var role = user.FindFirst(ClaimTypes.Role)!.Value;
		var userId = long.Parse(user.FindFirst(ClaimTypes.NameIdentifier)!.Value);
		
		var payload = await _payloadRepository.GetByIdAsync(id)
			?? throw new ApiException("Payload is not found", ApiException.NotFound);
			
		if (!(role == Roles.Admin || role == Roles.Manager || userId == payload.Order.ClientId))
		{
			throw new ApiException("No permission", ApiException.Forbidden);
		}

		return _mapper.Map<GetPayloadOrderDto>(payload);
	}

	public async Task<GetPayloadDto> UpdateAsync(long id, ClaimsPrincipal user, UpdatePayloadDto payloadDto)
	{		
		var role = user.FindFirst(ClaimTypes.Role)!.Value;
		var userId = long.Parse(user.FindFirst(ClaimTypes.NameIdentifier)!.Value);

		var payload = await _payloadRepository.GetByIdAsync(id)
			?? throw new ApiException("Payload is not found", ApiException.NotFound);
			
		if (!(role == Roles.Admin || role == Roles.Manager || userId == payload.Order.ClientId))
		{
			throw new ApiException("No permission", ApiException.Forbidden);
		}
		
		payload = _mapper.Map<Payload>(payloadDto);
		payload.Id = id;

		payload = await _payloadRepository.UpdateAsync(payload);

		return _mapper.Map<GetPayloadDto>(payload);
	}

}