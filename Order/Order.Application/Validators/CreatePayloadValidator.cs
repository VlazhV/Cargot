using FluentValidation;
using Order.Application.DTOs.PayloadDTOs;

namespace Order.Application.Validators;

public class CreatePayloadValidator: AbstractValidator<UpdatePayloadDto>
{
	public CreatePayloadValidator()
	{
		RuleFor(payloadDto => payloadDto.Height)			
			.ExclusiveBetween(0, 20_000);
			
		RuleFor(payloadDto => payloadDto.Width)
			.ExclusiveBetween(0, 20_000);
		
		RuleFor(payloadDto => payloadDto.Length)
			.ExclusiveBetween(0, 20_000);
			
		RuleFor(payloadDto => payloadDto.Weight)
			.ExclusiveBetween(0, 50_000_000);
	}	
}