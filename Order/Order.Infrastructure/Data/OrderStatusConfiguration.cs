using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Order.Domain.Constants;
using Order.Domain.Entities;

namespace Order.Infrastructure.Data;

public class OrderStatusConfiguration : IEntityTypeConfiguration<OrderStatus>
{
	public void Configure(EntityTypeBuilder<OrderStatus> builder)
	{
		builder.HasData(
			OrderStatuses.Processing,
			OrderStatuses.Accepted,
			OrderStatuses.Declined
		);
	}

}
