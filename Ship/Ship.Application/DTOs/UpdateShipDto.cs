namespace Ship.Application.DTOs;

public class UpdateShipDto
{
	public DateTime PlannedStart { get; set; }
	public DateTime PlannedFinish { get; set; }
	
	public long[] Orders { get; set; } = null!;
	
	public int CarId { get; set; }
	public int TrailerId { get; set; }
	public int AutoparkId { get; set; }
}
