using Autopark.Business.DTOs.CarDTOs;
using FluentValidation;

namespace Autopark.Business.Validators;

public class UpdateCarValidator: AbstractValidator<UpdateCarDto>
{
	public UpdateCarValidator()
	{
		RuleFor(c => c.LicenseNumber)
			.NotEmpty()
			.Length(5, 10);

		RuleFor(c => c.Mark)
			.NotEmpty();

		RuleFor(c => c.CapacityHeight)
			.Must(h => 500 <= h && h <= 2_500);

		RuleFor(c => c.CapacityLength)
			.Must(l => 500 <= l && l <= 6_000);

		RuleFor(c => c.CapacityWidth)
			.Must(w => 500 <= w && w <= 2_000);

		RuleFor(c => c.Carrying)
			.Must(c => 500_000 <= c && c <= 5_000_000);

		RuleFor(c => c.Range)
			.Must(r => 400 <= r && r <= 2000);

		RuleFor(c => c.TankVolume)
			.Must(v => 20 < v && v <= 2000);			
	}
}
