using FluentValidation;
using FluentValidation.Validators;
using Identity.Business.DTOs;
using Identity.DataAccess.Entities;

namespace Identity.Business.Validators;

public class RegisterValidator : AbstractValidator<RegisterDto>
{
	public RegisterValidator()
	{
		RuleFor(dto => dto.Email)
			.NotEmpty()
			.EmailAddress(EmailValidationMode.AspNetCoreCompatible);
		
		RuleFor(dto => dto.PhoneNumber)
			.NotEmpty()
			.Matches(@"([+]?[0-9\s-\(\)]{3,25})*$");
		
		RuleFor(dto => dto.UserName).NotEmpty();
		
		RuleFor(dto => dto.Role)
			.NotEmpty()
			.Must(r =>
				r!.Equals(Role.Admin)
				|| r!.Equals(Role.Manager)
				|| r!.Equals(Role.Client)
				|| r!.Equals(Role.Driver)
			);
	}
	
	
}