using Microsoft.EntityFrameworkCore;
using Autopark.DataAccess.Entities;

namespace Autopark.DataAccess.Data;

public class DatabaseContext: DbContext
{
	public DbSet<Entities.Autopark> Autoparks{ get; set; }
	public DbSet<Car> Cars{ get; set; }
	public DbSet<Trailer> Trailers{ get; set; }
	public DbSet<CarInShipShedule> CarShedule { get; set; }
	public DbSet<TrailerInShipShedule> TrailerShedule { get; set; }
	
	public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
	{		
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<Car>()
			.HasIndex(c => c.LicenseNumber)
			.IsUnique();

		modelBuilder.Entity<Trailer>()
			.HasIndex(t => t.LicenseNumber)
			.IsUnique();			
	}
}
