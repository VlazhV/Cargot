namespace Autopark.DataAccess.Entities;

public class Car
{
	public int Id{ get; set; }
	public string Mark { get; set; } = null!;	
	public int Range { get; set; } //km
	public int Carrying { get; set; } //mm	
	public int TankVolume { get; set; } //dm3
	public Capacity Capacity { get; set; } = null!;

	public Autopark Autopark { get; set; } = null!;
	public int AutoparkId { get; set; } 
}
