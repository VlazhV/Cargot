using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Ship.Domain.Entities;

namespace Ship.Infrastructure.Data;

public class DatabaseContext: DbContext
{
	public DbSet<Domain.Entities.Ship> Ships{ get; set; }
	public DbSet<Car> Cars { get; set; }
	public DbSet<Trailer> Trailers { get; set; }
	public DbSet<Driver> Drivers { get; set; }
	public DbSet<RouteStop> RouteStops { get; set; }
	public DbSet<RouteStopType> RouteStopTypes { get; set; }
	
	public DatabaseContext(DbContextOptions<DatabaseContext> options): base(options)
	{		
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);
		modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
	}
}
