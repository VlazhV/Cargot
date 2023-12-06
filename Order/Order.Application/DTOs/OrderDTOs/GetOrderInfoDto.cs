using Order.Application.DTOs.PayloadDTOs;
using Order.Application.DTOs.UserDTOs;

namespace Order.Application.DTOs.OrderDTOs;

public class GetOrderInfoDto
{
	public long Id { get; set; }
	
	public DateTime Time { get; set; }
	public DateTime AcceptTime { get; set; }
	
	public string LoadAddress { get; set; } = null!;
	public string DeliverAddress { get; set; } = null!;
	
	public DateTime LoadTime { get; set; }
	public DateTime DeliverTime { get; set; }
	
	public string OrderStatus { get; set; } = null!;
	public List<GetPayloadDto> Payloads { get; set; } = null!;
	public GetUserDto Client { get; set; } = null!;
}
