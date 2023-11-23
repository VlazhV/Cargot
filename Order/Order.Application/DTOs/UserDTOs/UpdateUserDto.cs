namespace Order.Application.DTOs.UserDTOs;

public class UpdateUserDto
{
	public string UserName { get; set; } = null!;
	public string Email { get; set; } = null!;
	public string PhoneNumber { get; set; } = null!;
}
