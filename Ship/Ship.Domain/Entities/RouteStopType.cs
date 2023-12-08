namespace Ship.Domain.Entities;

public class RouteStopType
{
	public short Id { get; set; }
	public string Name { get; set; } = null!;
	
	public IEnumerable<RouteStop>? RouteStops { get; set; }
}
