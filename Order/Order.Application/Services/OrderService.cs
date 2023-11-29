using System.Security.Claims;
using AutoMapper;
using Order.Application.DTOs.OrderDTOs;
using Order.Application.DTOs.PayloadDTOs;
using Order.Application.Exceptions;
using Order.Application.Interfaces;
using Order.Domain.Constants;
using Order.Domain.Entities;
using Order.Domain.Interfaces;
namespace Order.Application.Services;

public class OrderService : IOrderService
{
	private readonly IPayloadService _payloadService;
	private readonly IOrderRepository _orderRepository;
	private readonly IUserRepository _userRepository;
	private readonly IMapper _mapper;

	public OrderService
		(IPayloadService payloadService,
		IOrderRepository orderRepository,
		IUserRepository userRepository,
		IMapper mapper)
	{
		_payloadService = payloadService;
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
			if (role == Roles.Admin || role == Roles.Manager)
			{
				clientId = customerId.Value;
			}
			else
			{
				throw new ApiException("No permission", ApiException.Forbidden);
			}
		}
		else
		{
			clientId = long.Parse(user.FindFirst(ClaimTypes.NameIdentifier)!.Value);
		}

		if (!_userRepository.DoesItExist(clientId))
		{
			throw new ApiException("User is not found", ApiException.NotFound);
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

		var order = await _orderRepository.GetByIdAsync(id)
			?? throw new ApiException("Order is not found", ApiException.NotFound);

		if (!(role == Roles.Admin || role == Roles.Manager || userId == order.ClientId))
			throw new ApiException("No permission", ApiException.Forbidden);

		_orderRepository.Delete(order);
		await _orderRepository.SaveChangesAsync();
	}

	public async Task<IEnumerable<GetOrderInfoDto>> GetAllAsync()
	{
		var orders = await _orderRepository.GetAllAsync();

		return orders.Select(o => _mapper.Map<GetOrderInfoDto>(o));
	}

	public async Task<GetOrderInfoDto> GetByIdAsync(long id, ClaimsPrincipal user)
	{
		var role = user.FindFirst(ClaimTypes.Role)!.Value;
		var userId = long.Parse(user.FindFirst(ClaimTypes.NameIdentifier)!.Value); 		
		
		var order = await _orderRepository.GetByIdAsync(id)
			?? throw new ApiException("Order is not found", ApiException.NotFound);
			
		if (!(role == Roles.Admin || role == Roles.Manager || userId == order.ClientId))
		{
			throw new ApiException("No permission", ApiException.Forbidden);
		}

		return _mapper.Map<GetOrderInfoDto>(order);
	}

	public async Task<GetOrderDto> SetStatusAsync(long id, string status, ClaimsPrincipal user)
	{
		var role = user.FindFirst(ClaimTypes.Role)!.Value;

		if (!(role == Roles.Admin || role == Roles.Manager))
		{
			throw new ApiException("No permission", ApiException.Forbidden);
		}

		var order = await _orderRepository.GetByIdAsync(id)
			?? throw new ApiException("Order is not found", ApiException.NotFound);

		order = await _orderRepository.SetStatusAsync(order, status.ToLower())
			?? throw new ApiException("Incorrect orderStatus", ApiException.BadRequest);

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
			?? throw new ApiException("Order is not found", ApiException.NotFound);

		if (!(role == Roles.Admin || role == Roles.Manager || userId == order.ClientId))
		{
			throw new ApiException("No permission", ApiException.Forbidden);
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
			?? throw new ApiException("Order is not found", ApiException.NotFound);

		if (!(role == Roles.Admin || role == Roles.Manager || userId == order.ClientId))
		{
			throw new ApiException("No permission", ApiException.Forbidden);
		}

		_orderRepository.ClearPayloadList(order);
		
		foreach(var payloadDto in payloadDtos)
		{
			var createPayloadDto = _mapper.Map<CreatePayloadDto>(payloadDto);
			createPayloadDto.OrderId = id;
			await _payloadService.CreateAsync(createPayloadDto);
		}

		await _orderRepository.SaveChangesAsync();
		
		order = await _orderRepository.GetByIdAsync(id);

		return _mapper.Map<GetOrderInfoDto>(order);
	}

}
