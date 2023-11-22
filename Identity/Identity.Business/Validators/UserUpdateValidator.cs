using FluentValidation;
using FluentValidation.Validators;
using Identity.Business.DTOs;

namespace Identity.Business.Validators;

public class UserUpdateValidator: AbstractValidator<UserUpdateDto>
{
    public UserUpdateValidator()
    {
        RuleFor(dto => dto.Email)
			.NotEmpty()
			.EmailAddress(EmailValidationMode.AspNetCoreCompatible);
		
		RuleFor(dto => dto.PhoneNumber)
			.NotEmpty()
			.Matches(@"([+]?[0-9\s-\(\)]{3,25})*$");
		
		RuleFor(dto => dto.UserName).NotEmpty();
    }
}