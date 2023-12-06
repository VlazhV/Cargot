using Microsoft.EntityFrameworkCore;
using Autopark.DataAccess.Entities;
using System.Reflection;

namespace Autopark.DataAccess.Data;

public class DatabaseContext: DbContext
{
	public DbSet<Entities.Autopark> Autoparks{ get; set; }
	public DbSet<Car> Cars{ get; set; }
	public DbSet<Trailer> Trailers{ get; set; }
	public DbSet<CarInShipSchedule> CarSchedule { get; set; }
	public DbSet<TrailerInShipSchedule> TrailerSchedule { get; set; }
	
	public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
	{		
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);
		modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());		
	}
}
