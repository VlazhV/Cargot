using Autopark.Business.DTOs.CarDTOs;
using Autopark.Business.DTOs.TrailerDTOs;

namespace Autopark.Business.DTOs.AutoparkDTOs;

public class GetAutoparkVehicleDto
{
	public int Id { get; set; }
	public string Address { get; set; } = null!;

	public IEnumerable<GetCarDto> Cars { get; set; } = null!;
	public IEnumerable<GetTrailerDto> Trailers { get; set; } = null!;
}
