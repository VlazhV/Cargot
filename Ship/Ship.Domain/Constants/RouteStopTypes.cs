using Ship.Domain.Entities;

namespace Ship.Domain.Constants;

public class RouteStopTypes
{
	public static RouteStopType Loading { get => new RouteStopType() { Id = 1, Name = "loading" }; }
	public static RouteStopType Unloading { get => new RouteStopType() { Id = 1, Name = "unloading" }; }
	public static RouteStopType Refill { get => new RouteStopType() { Id = 1, Name = "refill" }; }
}
