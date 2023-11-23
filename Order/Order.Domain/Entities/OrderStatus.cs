namespace Order.Domain.Entities;

public class OrderStatus
{
	public const string Processing = "processing";
	public const string Accepted = "accepted";
	public const string Declined = "declined";
	
	public ushort Id { get; set; }
	public string Name { get; set; } = null!;
}
