using FluentValidation;
using Order.Application.DTOs.OrderDTOs;

namespace Order.Application.Validators;

public class UpdateOrderPayloadsValidator: AbstractValidator<UpdateOrderPayloadsDto>
{
	public UpdateOrderPayloadsValidator()
	{
		RuleFor(orderDto => orderDto.LoadTime)
			.GreaterThan(DateTime.UtcNow);

		RuleFor(orderDto => orderDto.DeliverTime)
			.GreaterThan(orderDto => orderDto.LoadTime);

		RuleFor(orderDto => orderDto.LoadAddress)
			.NotEmpty();
		
		RuleFor(orderDto => orderDto.DeliverAddress)
			.NotEmpty()
			.NotEqual(orderDto => orderDto.LoadAddress);

		RuleFor(orderDto => orderDto.Payloads)
			.NotEmpty();
	}
}