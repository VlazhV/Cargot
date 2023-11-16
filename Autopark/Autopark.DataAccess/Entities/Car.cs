namespace Autopark.DataAccess.Entities;

public class Car
{
	public int Id{ get; set; }
	public string LicenseNumber { get; set; } = null!;
	public string Mark { get; set; } = null!;	
	public int Range { get; set; } //km
	public int Carrying { get; set; } //g
	public int TankVolume { get; set; } //dm3	
	
	public int CapacityLength { get; set; } //mm	
	public int CapacityWidth { get; set; } //mm	
	public int CapacityHeight { get; set; } //mm

	public Autopark Autopark { get; set; } = null!;
	public int AutoparkId { get; set; }

	public List<CarInShipShedule> Shedules { get; set; } = null!;
}
