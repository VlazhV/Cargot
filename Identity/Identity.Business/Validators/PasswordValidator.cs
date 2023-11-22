using FluentValidation;
using Identity.Business.DTOs;

namespace Identity.Business.Validators;

public class PasswordValidator: AbstractValidator<PasswordDto>
{
	public PasswordValidator()
	{
		RuleFor(dto => dto.OldPassword).NotEmpty();
		
		RuleFor(dto => dto.NewPassword).NotEmpty()
			.NotEqual(dto => dto.OldPassword);
			
	}
}