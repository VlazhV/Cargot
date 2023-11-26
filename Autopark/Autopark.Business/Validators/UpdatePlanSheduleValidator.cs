using Autopark.Business.DTOs.ScheduleDtos;
using FluentValidation;

namespace Autopark.Business.Validators;

public class UpdatePlanScheduleValidator: AbstractValidator<UpdatePlanScheduleDto>
{
	public UpdatePlanScheduleValidator()
	{
		RuleFor(s => s.PlanStart)
			.GreaterThan(DateTime.UtcNow);
		
		RuleFor(s => s.PlanFinish)
			.GreaterThan(s => s.PlanStart);
	}
}