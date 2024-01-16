namespace Ship.Application.DTOs;

public class GetShipDto
{
	public string Id { get; set; } = null!;

	public DateTime PlannedStart { get; set; }
	public DateTime PlannedFinish { get; set; }

	public DateTime? Start { get; set; }
	public DateTime? Finish { get; set; }

	public long[] Orders { get; set; } = null!;

	public int AutoparkId { get; set; }
	public int CarId { get; set; }
	public int TrailerId { get; set; }
}
