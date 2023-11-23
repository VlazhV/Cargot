using Microsoft.EntityFrameworkCore;
using Order.Domain.Entities;

namespace Order.Infrastructure.Data;

public class DatabaseContext: DbContext
{
	public DbSet<Domain.Entities.Order> Orders { get; set; }
	public DbSet<User> Users{ get; set; }
	public DbSet<Payload> Payloads { get; set; }
	public DbSet<OrderStatus> OrderStatuses { get; set; }

	public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
	{
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<OrderStatus>()
			.HasData(
				new OrderStatus { Id = 1, Name = OrderStatus.Processing },
				new OrderStatus { Id = 2, Name = OrderStatus.Accepted },
				new OrderStatus { Id = 3, Name = OrderStatus.Declined }
			);
	}
}
