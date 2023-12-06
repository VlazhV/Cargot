namespace Order.Application.DTOs.OrderDTOs;

public class GetOrderDto
{
	public long Id { get; set; }
	
	public DateTime Time { get; set; }
	public DateTime AcceptTime { get; set; }
	
	public string LoadAddress { get; set; } = null!;
	public string DeliverAddress { get; set; } = null!;
	
	public DateTime LoadTime { get; set; }
	public DateTime DeliverTime { get; set; }

	public string OrderStatus { get; set; } = null!;
}
