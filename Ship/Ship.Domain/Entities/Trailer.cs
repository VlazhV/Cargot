namespace Ship.Domain.Entities;

public class Trailer
{
	public int Id { get; set; }
	public string LicenseNumber { get; set; } = null!;

	public int CapacityLength { get; set; } //mm
	public int CapacityWidth { get; set; } //mm	
	public int CapacityHeight { get; set; } //mm	
	
	public int Carrying { get; set; }
	
	public IEnumerable<Ship>? Ships{ get; set; }
}
