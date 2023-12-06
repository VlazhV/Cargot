using Autopark.Business.DTOs;
using FluentValidation;

namespace Autopark.Business.Validators;

public class SpecValidator: AbstractValidator<SpecDto>
{
	public SpecValidator()
	{
		RuleFor(spec => spec.Start)
			.Must((spec, start) =>
			{
				if (spec.FreeOnly && start is null)
					return false;

				return true;
			});
			
		RuleFor(spec => spec.Finish)
			.Must((spec, finish) =>
			{
				if (spec.FreeOnly && finish is null)
					return false;

				return true;
			});
			
	}
}