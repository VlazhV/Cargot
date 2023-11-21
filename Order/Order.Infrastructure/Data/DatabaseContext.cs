using Microsoft.EntityFrameworkCore;
using Order.Domain.Entities;

namespace Order.Infrastructure.Data;

public class DatabaseContext: DbContext
{
	public DbSet<Domain.Entities.Order> Orders { get; set; }
	public DbSet<User> Users{ get; set; }
	public DbSet<Payload> Payloads { get; set; }
	public DbSet<OrderStatus> OrderStatuses { get; set; }
}
