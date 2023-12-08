namespace Ship.Domain.Entities;

public class Ship
{
	public long Id { get; set; }

	public Car Car { get; set; } = null!;
	public int CarId { get; set; }
	
	public Trailer? Trailer{ get; set; }
	public int? TrailerId { get; set; }

	public IEnumerable<RouteStop> RouteStops { get; set; } = null!;

	public IEnumerable<Driver> Drivers { get; set; } = null!;
}
