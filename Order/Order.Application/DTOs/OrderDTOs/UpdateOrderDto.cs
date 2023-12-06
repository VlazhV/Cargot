namespace Order.Application.DTOs.OrderDTOs;

public class UpdateOrderDto
{
	public string LoadAddress { get; set; } = null!;
	public string DeliverAddress { get; set; } = null!;
	
	public DateTime LoadTime { get; set; }
	public DateTime DeliverTime { get; set; }
}
