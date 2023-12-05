using System.Security.Claims;
using AutoMapper;
using Order.Application.Constants;
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

	public async Task<GetPayloadDto> CreateAsync(CreatePayloadDto payloadDto)
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
			?? throw new ApiException(Messages.PayloadIsNotFound, ApiException.NotFound);

		if (!(role == Roles.Admin || role == Roles.Manager || userId == payload.Order.ClientId))
		{
			throw new ApiException(Messages.NoPermission, ApiException.Forbidden);
		}

		_payloadRepository.Delete(payload);
		await _payloadRepository.SaveChangesAsync();
	}

	public async Task<IEnumerable<GetPayloadOrderDto>> GetAllAsync()
	{
		var payloads = await _payloadRepository.GetAllAsync();
		
		return _mapper.Map<IEnumerable<GetPayloadOrderDto>>(payloads);
	}

	public async Task<GetPayloadOrderDto> GetByIdAsync(long id, ClaimsPrincipal user)
	{
		var role = user.FindFirst(ClaimTypes.Role)!.Value;
		var userId = long.Parse(user.FindFirst(ClaimTypes.NameIdentifier)!.Value);
		
		var payload = await _payloadRepository.GetByIdAsync(id)
			?? throw new ApiException(Messages.PayloadIsNotFound, ApiException.NotFound);
			
		if (!(role == Roles.Admin || role == Roles.Manager || userId == payload.Order.ClientId))
		{
			throw new ApiException(Messages.NoPermission, ApiException.Forbidden);
		}

		return _mapper.Map<GetPayloadOrderDto>(payload);
	}

	public async Task<GetPayloadDto> UpdateAsync(long id, ClaimsPrincipal user, UpdatePayloadDto payloadDto)
	{		
		var role = user.FindFirst(ClaimTypes.Role)!.Value;
		var userId = long.Parse(user.FindFirst(ClaimTypes.NameIdentifier)!.Value);

		var payload = await _payloadRepository.GetByIdAsync(id)
			?? throw new ApiException(Messages.PayloadIsNotFound, ApiException.NotFound);
			
		if (!(role == Roles.Admin || role == Roles.Manager || userId == payload.Order.ClientId))
		{
			throw new ApiException(Messages.NoPermission, ApiException.Forbidden);
		}
		
		var updatedPayload = _mapper.Map<Payload>(payloadDto);
		updatedPayload.Id = id;		
		updatedPayload.OrderId = payload.OrderId;

		payload = _payloadRepository.Update(updatedPayload);
		await _payloadRepository.SaveChangesAsync();

		return _mapper.Map<GetPayloadDto>(updatedPayload);
	}
}