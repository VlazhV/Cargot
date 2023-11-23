using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Order.Domain.Entities;

namespace Order.Infrastructure.Data;

public class OrderStatusConfiguration : IEntityTypeConfiguration<OrderStatus>
{
    public void Configure(EntityTypeBuilder<OrderStatus> builder)
    {
        builder.HasData(
            new OrderStatus { Id = 1, Name = OrderStatus.Processing },
            new OrderStatus { Id = 2, Name = OrderStatus.Accepted },
            new OrderStatus { Id = 3, Name = OrderStatus.Declined }
        );
    }

}
