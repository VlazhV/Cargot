namespace Ship.Application.DTOs;

public class GenerateShipDto
{
	public long[] Orders { get; set; } = null!;
	public DateTime PlannedStart { get; set; }
	public DateTime PlannedFinish { get; set; }
}
