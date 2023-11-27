using System.Net.NetworkInformation;
using AutoMapper;
using Order.Application.DTOs.OrderDTOs;
using Order.Application.DTOs.PayloadDTOs;
using Order.Application.Exceptions;
using Order.Application.Interfaces;
using Order.Domain.Entities;
using Order.Domain.Interfaces;
namespace Order.Application.Services;

public class OrderService: IOrderService
{
	private readonly IPayloadService _payloadService;
	private readonly IOrderRepository _orderRepository;
	private readonly IMapper _mapper;

	public OrderService
		(IPayloadService payloadService,
		IOrderRepository orderRepository, 
		IMapper mapper)
	{
		_payloadService = payloadService;
		_orderRepository = orderRepository;
		_mapper = mapper;
	}

	public async Task<GetOrderDto> CreateAsync(UpdateOrderPayloadsDto orderDto)
	{
		var order = _mapper.Map<Domain.Entities.Order>(orderDto);
		order.OrderStatusId = OrderStatus.Processing.Id;
		order.Time = DateTime.UtcNow;
		
		order = await _orderRepository.CreateAsync(order);
		
		return _mapper.Map<GetOrderDto>(order);
	}

	public async Task DeleteAsync(long id)
	{
		var order = await _orderRepository.GetByIdAsync(id)
			?? throw new ApiException("Order is not found", ApiException.NotFound);

		await _orderRepository.DeleteAsync(order);		
	}

	public async Task<IEnumerable<GetOrderInfoDto>> GetAllAsync()
	{
		var orders = await _orderRepository.GetAllAsync();

		return orders.Select(o => _mapper.Map<GetOrderInfoDto>(o));
	}

	public async Task<GetOrderInfoDto> GetByIdAsync(long id)
	{
		var order = await _orderRepository.GetByIdAsync(id)
			?? throw new ApiException("Order is not found", ApiException.NotFound);

		return _mapper.Map<GetOrderInfoDto>(order);
	}

	public async Task<GetOrderDto> SetStatusAsync(long id, string status)
	{
		var order = await _orderRepository.GetByIdAsync(id)
			?? throw new ApiException("Order is not found", ApiException.NotFound);

		order = await _orderRepository.SetStatusAsync(order, status.ToLower())
			?? throw new ApiException("Incorrect orderStatus", ApiException.BadRequest);
			
		if (status.Equals(OrderStatus.Accepted.Name))
		{
			order.AcceptTime = DateTime.UtcNow;

			order = await _orderRepository.UpdateAsync(order);
		}

		return _mapper.Map<GetOrderDto>(order);
	}

	public async Task<GetOrderDto> UpdateAsync(long id, UpdateOrderDto orderDto)
	{
		var order = await _orderRepository.GetByIdAsync(id)
			?? throw new ApiException("Order is not found", ApiException.NotFound);

		order = _mapper.Map(orderDto, order);
		order = await _orderRepository.UpdateAsync(order);

		return _mapper.Map<GetOrderDto>(order);
	}

	public async Task<GetOrderInfoDto> UpdatePayloadListAsync(long id, IEnumerable<UpdatePayloadDto> payloadDtos)
	{
		var order = await _orderRepository.GetByIdAsync(id)
			?? throw new ApiException("Order is not found", ApiException.NotFound);

		await _orderRepository.ClearPayloadListAsync(order);
		
		foreach(var payloadDto in payloadDtos)
		{			
			await _payloadService.CreateAsync(payloadDto);
		}

		order = await _orderRepository.GetByIdAsync(id);

		return _mapper.Map<GetOrderInfoDto>(order);
	}

}
