using FluentValidation;
using Identity.Business.DTOs;

namespace Identity.Business.Validators;

public class LoginValidator: AbstractValidator<LoginDto>
{
	public LoginValidator()
	{
		RuleFor(dto => dto.UserName).NotEmpty();

		RuleFor(dto => dto.Password).NotEmpty();
	}
}