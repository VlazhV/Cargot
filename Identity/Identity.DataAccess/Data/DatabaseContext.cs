using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Identity.DataAccess.Data;

public class DatabaseContext: IdentityDbContext<IdentityUser<long>, IdentityRole<long>, long>
{
	public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }
	
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