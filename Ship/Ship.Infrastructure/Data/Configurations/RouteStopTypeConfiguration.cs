using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ship.Domain.Constants;
using Ship.Domain.Entities;

namespace Ship.Infrastructure.Data.Configurations;

public class RouteStopTypeConfiguration : IEntityTypeConfiguration<RouteStopType>
{
	public void Configure(EntityTypeBuilder<RouteStopType> builder)
	{
		builder.HasData(
			RouteStopTypes.Loading,
			RouteStopTypes.Unloading,
			RouteStopTypes.Refill
		);
	}
}
