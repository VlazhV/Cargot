using Order.Application.DTOs.OrderDTOs;

namespace Order.Application.DTOs.UserDTOs;

public class GetUserInfoDto
{
	public long Id { get; set; }
	public string UserName { get; set; } = null!;
	public string Email { get; set; } = null!;
	public string PhoneNumber { get; set; } = null!;

	public List<GetOrderInfoDto> Orders { get; set; } = null!;
}
