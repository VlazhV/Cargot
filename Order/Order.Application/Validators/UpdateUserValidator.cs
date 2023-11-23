using FluentValidation;
using FluentValidation.Validators;
using Order.Application.DTOs.UserDTOs;

namespace Order.Application.Validators;

public class UpdateUserValidator: AbstractValidator<UpdateUserDto>
{
	public UpdateUserValidator()
	{
		RuleFor(dto => dto.Email)
			.NotEmpty()
			.EmailAddress(EmailValidationMode.AspNetCoreCompatible);
		
		RuleFor(dto => dto.PhoneNumber)
			.NotEmpty()
			.Matches(@"^\+?\d{1,3}\(?\d{1,4}\)?\d{7}$");
		
		RuleFor(dto => dto.UserName).NotEmpty();
	}
}
