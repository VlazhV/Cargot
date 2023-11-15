namespace Autopark.Business.DTOs.CarDTOs;

public class UpdateCarDto
{
	public string? Mark { get; set; }
	public string? LicenseNumber { get; set; } 
	public int Range { get; set; } 
	public int Carrying { get; set; } 	
	public int TankVolume { get; set; } 
	public int CapacityLength { get; set; }
	public int CapacityWidth { get; set; }
	public int CapacityHeight { get; set; }
	public int AutoparkId { get; set; } 
}
