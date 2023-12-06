namespace Autopark.Business.DTOs.TrailerDTOs;

public class GetTrailerDto
{
	public int Id { get; set; }
	public string LicenseNumber { get; set; } = null!;
	public int CapacityLength { get; set; }
	public int CapacityWidth { get; set; }
	public int CapacityHeight { get; set; }
	public int Carrying { get; set; }
	public int AutoparkId { get; set; }
}
