using Autopark.Business.DTOs.TrailerDTOs;
using FluentValidation;

namespace Autopark.Business.Validators;

public class UpdateTrailerValidator: AbstractValidator<UpdateTrailerDto>
{
	public UpdateTrailerValidator()
	{
		RuleFor(t => t.LicenseNumber)
			.NotEmpty()
			.Length(6, 10);
			
		RuleFor(c => c.CapacityHeight)
			.Must(h => 2_000 <= h && h <= 4_000);

		RuleFor(c => c.CapacityLength)
			.Must(l => 6_000 <= l && l <= 14_000);

		RuleFor(c => c.CapacityWidth)
			.Must(w => 2_000 <= w && w <= 3_000);

		RuleFor(c => c.Carrying)
			.Must(c => 5_000_000 <= c && c <= 25_000_000);
	}
}
