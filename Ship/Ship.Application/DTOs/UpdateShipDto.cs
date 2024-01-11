namespace Ship.Application.DTOs;

public class UpdateShipDto
{
	public DateTime PlannedStart { get; set; }
	public DateTime PlannedFinish { get; set; }
	
	public IEnumerable<long> Orders { get; set; } = null!;
	
	public int AutoparkId { get; set; }
}
