using Autopark.Business.DTOs.AutoparkDTOs;
using FluentValidation;

namespace Autopark.Business.Validators;

public class UpdateAutoparkValidator: AbstractValidator<UpdateAutoparkDto>
{
	public UpdateAutoparkValidator()
	{
		RuleFor(a => a.Address)
			.NotEmpty()
			.Length(6, 256);
	}
}
