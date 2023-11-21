using System.Globalization;
namespace Order.Domain.Entities;

public class Order
{
	public long Id { get; set; }
	
	public DateTime Time { get; set; }
	public DateTime AcceptTime { get; set; }
	
	public string LoadAddress { get; set; } = null!;
	public string DeliverAddress { get; set; } = null!;
	
	public DateTime LoadTime { get; set; }
	public DateTime DeliverTime { get; set; }

	public List<Payload> Payloads { get; set; } = null!;
	
	public User Client { get; set; } = null!;
	public long ClientId { get; set; } 
	
	public OrderStatus OrderStatus { get; set; } = null!;
	public ushort OrderStatusId { get; set; }
	
}
