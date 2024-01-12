using Microsoft.EntityFrameworkCore;
using MongoDB.EntityFrameworkCore.Extensions;

namespace Ship.Infrastructure.Data;

public class DatabaseContext: DbContext
{
	public DbSet<Domain.Entities.Ship> Ships{ get; set; }
	
	public DatabaseContext(DbContextOptions<DatabaseContext> options): base(options)
	{		
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);
		modelBuilder.Entity<Domain.Entities.Ship>()
			.ToCollection("ships");

        modelBuilder.Entity<Domain.Entities.Ship>()
            .Property(ship => ship.Id)
            .ValueGeneratedOnAdd();
    }
}
