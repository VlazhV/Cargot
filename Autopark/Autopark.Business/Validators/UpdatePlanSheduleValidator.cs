using Autopark.Business.DTOs.SheduleDtos;
using FluentValidation;

namespace Autopark.Business.Validators;

public class UpdatePlanSheduleValidator: AbstractValidator<UpdatePlanSheduleDto>
{
	public UpdatePlanSheduleValidator()
	{
		RuleFor(s => s.PlanStart)
			.GreaterThan(DateTime.UtcNow);
		
		RuleFor(s => s.PlanFinish)
			.GreaterThan(s => s.PlanStart);
	}
}