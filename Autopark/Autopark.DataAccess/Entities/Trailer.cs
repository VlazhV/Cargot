namespace Autopark.DataAccess.Entities;

public class Trailer
{
	public int Id { get; set; }
	public string LicenseNumber { get; set; } = null!;
	public Capacity Capacity { get; set; } = null!;
	public int Carrying { get; set; }

	public Autopark Autopark { get; set; } = null!;
	public int AutoparkId { get; set; }
}
