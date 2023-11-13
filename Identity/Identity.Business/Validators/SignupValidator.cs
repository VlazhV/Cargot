using FluentValidation;
using FluentValidation.Validators;
using Identity.Business.DTOs;

namespace Identity.Business.Validators;

public class SignupValidator: AbstractValidator<SignupDto>
{
	public SignupValidator()
	{
		RuleFor(dto => dto.Email)
			.NotEmpty()
			.EmailAddress(EmailValidationMode.AspNetCoreCompatible);
		
		RuleFor(dto => dto.PhoneNumber)
			.NotEmpty()
			.Matches(@"^\+?\d{1,3}\(?\d{1,4}\)?\d{7}$");
		
		RuleFor(dto => dto.UserName).NotEmpty();
		
		RuleFor(dto => dto.Password).NotEmpty();		
	}
}