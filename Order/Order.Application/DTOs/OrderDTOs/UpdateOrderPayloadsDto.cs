using Order.Application.DTOs.PayloadDTOs;

namespace Order.Application.DTOs.OrderDTOs;

public class UpdateOrderPayloadsDto
{
	public string LoadAddress { get; set; } = null!;
	public string DeliverAddress { get; set; } = null!;
	
	public DateTime LoadTime { get; set; }
	public DateTime DeliverTime { get; set; }
	public List<CreatePayloadDto> Payloads { get; set; } = null!;
}