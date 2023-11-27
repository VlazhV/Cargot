using Order.Domain.Entities;

namespace Order.Domain.Constants;

public static class OrderStatuses
{
    public static OrderStatus Processing { get => new OrderStatus() { Id = 1, Name = "processing" }; }
	public static OrderStatus Accepted { get => new OrderStatus() { Id = 2, Name = "accepted" }; }
	public static OrderStatus Declined { get => new OrderStatus() { Id = 3, Name = "declined" }; }   
}