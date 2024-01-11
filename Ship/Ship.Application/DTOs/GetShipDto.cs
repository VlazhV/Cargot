namespace Ship.Application.DTOs;

public class GetShipDto
{
	public long Id { get; set; }
	
	public DateTime PlannedStart { get; set; }
	public DateTime PlannedFinish { get; set; }
	
	public DateTime? Start { get; set; }
	public DateTime? Finish { get; set; }

	public IEnumerable<long> Orders { get; set; } = null!;
	
	public int AutoparkId { get; set; }
}
