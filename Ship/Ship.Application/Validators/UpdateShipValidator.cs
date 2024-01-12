using FluentValidation;
using Ship.Application.DTOs;

namespace Ship.Application.Validators;

public class UpdateShipValidator: AbstractValidator<UpdateShipDto>
{
	public UpdateShipValidator()
	{
		RuleFor(dto => dto.PlannedStart)
			.GreaterThan(DateTime.UtcNow);

		RuleFor(dto => dto.PlannedFinish)
			.GreaterThan(dto => dto.PlannedStart);

		RuleFor(dto => dto.Orders)
			.NotEmpty();
	}
}
