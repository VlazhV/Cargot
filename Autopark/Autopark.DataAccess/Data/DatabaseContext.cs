using Microsoft.EntityFrameworkCore;
using Autopark.DataAccess.Entities;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

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

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		if (!optionsBuilder.IsConfigured)
		{
			IConfigurationRoot configuration = new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile("appsettings.json")
				.Build();

			var connectionString = configuration.GetConnectionString("Default");
			optionsBuilder.UseSqlServer(connectionString);
		}
	}

}
