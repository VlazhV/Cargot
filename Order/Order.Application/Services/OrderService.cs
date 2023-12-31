using System.Security.Claims;
using AutoMapper;
using Order.Application.DTOs.OrderDTOs;
using Order.Application.DTOs.PayloadDTOs;
using Order.Application.Exceptions;
using Order.Application.Interfaces;
using Order.Domain.Constants;
using Order.Application.Constants;
using Order.Domain.Interfaces;
using Order.Domain.Entities;
namespace Order.Application.Services;

public class OrderService : IOrderService
{
	private readonly IPayloadRepository _payloadRepository;
	private readonly IOrderRepository _orderRepository;
	private readonly IUserRepository _userRepository;
	private readonly IMapper _mapper;

	public OrderService
		(IPayloadRepository payloadRepository,
		IOrderRepository orderRepository,
		IUserRepository userRepository,
		IMapper mapper)
	{
		_payloadRepository = payloadRepository;
		_orderRepository = orderRepository;
		_userRepository = userRepository;
		_mapper = mapper;
	}

	public async Task<GetOrderDto> CreateAsync(long? customerId, ClaimsPrincipal user, UpdateOrderPayloadsDto orderDto)
	{
		long clientId;

		if (customerId.HasValue)
		{
			var role = user.FindFirst(ClaimTypes.Role)!.Value;
		
			if (!(role == Roles.Admin || role == Roles.Manager))
			{
				throw new ApiException(Messages.NoPermission, ApiException.Forbidden);
			}
			
			clientId = customerId.Value;
		}
		else
		{
			clientId = long.Parse(user.FindFirst(ClaimTypes.NameIdentifier)!.Value);
		}

		if (! await _userRepository.IsUserExistsAsync(clientId))
		{
			throw new ApiException(Messages.UserIsNotFound, ApiException.NotFound);
		}

		var order = _mapper.Map<Domain.Entities.Order>(orderDto);
		order.ClientId = clientId;
		order.OrderStatusId = OrderStatuses.Processing.Id;
		order.Time = DateTime.UtcNow;

		order = await _orderRepository.CreateAsync(order);
		order.OrderStatus = OrderStatuses.Processing;
		await _orderRepository.SaveChangesAsync();
		
		return _mapper.Map<GetOrderDto>(order);
	}

	public async Task DeleteAsync(long id, ClaimsPrincipal user)
	{
		var role = user.FindFirst(ClaimTypes.Role)!.Value;

		var userId = long.Parse(user.FindFirst(ClaimTypes.NameIdentifier)!.Value);

		var order = await _orderRepository.GetByIdAsync(id);
		
		if (order is null)
		{
			throw new ApiException(Messages.OrderIsNotFound, ApiException.NotFound);	
		}

		if (!(role == Roles.Admin || role == Roles.Manager || userId == order.ClientId))
		{
			throw new ApiException(Messages.NoPermission, ApiException.Forbidden);
		}
			

		_orderRepository.Delete(order);
		await _orderRepository.SaveChangesAsync();
	}

	public async Task<IEnumerable<GetOrderInfoDto>> GetAllAsync()
	{
		var orders = await _orderRepository.GetAllAsync();

		return _mapper.Map<IEnumerable<GetOrderInfoDto>>(orders);
	}

	public async Task<GetOrderInfoDto> GetByIdAsync(long id, ClaimsPrincipal user)
	{
		var role = user.FindFirst(ClaimTypes.Role)!.Value;
		var userId = long.Parse(user.FindFirst(ClaimTypes.NameIdentifier)!.Value);

		var order = await _orderRepository.GetByIdAsync(id);
		
		if (order is null)
		{
			throw new ApiException(Messages.OrderIsNotFound, ApiException.NotFound);
		}			
			
		if (!(role == Roles.Admin || role == Roles.Manager || userId == order.ClientId))
		{
			throw new ApiException(Messages.NoPermission, ApiException.Forbidden);
		}

		return _mapper.Map<GetOrderInfoDto>(order);
	}

	public async Task<GetOrderDto> SetStatusAsync(long id, string status, ClaimsPrincipal user)
	{
		var role = user.FindFirst(ClaimTypes.Role)!.Value;

		if (!(role == Roles.Admin || role == Roles.Manager))
		{
			throw new ApiException(Messages.NoPermission, ApiException.Forbidden);
		}

		var order = await _orderRepository.GetByIdAsync(id)
			?? throw new ApiException(Messages.OrderIsNotFound, ApiException.NotFound);

		order = await _orderRepository.SetStatusAsync(order, status.ToLower())
			?? throw new ApiException(Messages.IncorrectOrderStatus, ApiException.BadRequest);

		if (status.Equals(OrderStatuses.Accepted.Name))
		{
			order.AcceptTime = DateTime.UtcNow;

			order = _orderRepository.Update(order);
		}

		await _orderRepository.SaveChangesAsync();

		return _mapper.Map<GetOrderDto>(order);
	}

	public async Task<GetOrderDto> UpdateAsync(long id, ClaimsPrincipal user, UpdateOrderDto orderDto)
	{
		var role = user.FindFirst(ClaimTypes.Role)!.Value;
		var userId = long.Parse(user.FindFirst(ClaimTypes.NameIdentifier)!.Value);

		var order = await _orderRepository.GetByIdAsync(id)
			?? throw new ApiException(Messages.OrderIsNotFound, ApiException.NotFound);

		if (!(role == Roles.Admin || role == Roles.Manager || userId == order.ClientId))
		{
			throw new ApiException(Messages.NoPermission, ApiException.Forbidden);
		}

		order = _mapper.Map(orderDto, order);
		order = _orderRepository.Update(order);
		await _orderRepository.SaveChangesAsync();

		return _mapper.Map<GetOrderDto>(order);
	}

	public async Task<GetOrderInfoDto> UpdatePayloadListAsync(long id, ClaimsPrincipal user, IEnumerable<UpdatePayloadDto> payloadDtos)
	{
		var role = user.FindFirst(ClaimTypes.Role)!.Value;
		var userId = long.Parse(user.FindFirst(ClaimTypes.NameIdentifier)!.Value);

		var order = await _orderRepository.GetByIdAsync(id)
			?? throw new ApiException(Messages.OrderIsNotFound, ApiException.NotFound);

		if (!(role == Roles.Admin || role == Roles.Manager || userId == order.ClientId))
		{
			throw new ApiException(Messages.NoPermission, ApiException.Forbidden);
		}

		_orderRepository.ClearPayloadList(order);
		var payloads = _mapper.Map<IEnumerable<Payload>>(payloadDtos);
		
		foreach(var payload in payloads)
		{
			payload.OrderId = id;
		}

		await _payloadRepository.CreateManyAsync(payloads);		
		await _orderRepository.SaveChangesAsync();
		
		order = await _orderRepository.GetByIdAsync(id);

		return _mapper.Map<GetOrderInfoDto>(order);
	}
}
