namespace Ship.Domain.Entities;

public class RouteStop
{
	public long Id { get; set; }
	public string Address { get; set; } = null!;

	public short RouteStopTypeId { get; set; }
	public RouteStopType RouteStopType { get; set; } = null!;
	
	public long ShipId { get; set; }
	public Ship Ship { get; set; } = null!;
}
