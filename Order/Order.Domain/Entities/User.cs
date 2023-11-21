namespace Order.Domain.Entities;

public class User
{
	public long Id { get; set; }
	public string UserName { get; set; } = null!;
	public string Email { get; set; } = null!;
	public string PhoneNumber { get; set; } = null!;
	
	public List<Order>? Orders { get; set; }
}
