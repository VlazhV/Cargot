namespace Order.Application.DTOs.UserDTOs;

public class GetUserDto
{
	public long Id { get; set; }
	public string UserName { get; set; } = null!;
	public string Email { get; set; } = null!;
	public string PhoneNumber { get; set; } = null!;
}
