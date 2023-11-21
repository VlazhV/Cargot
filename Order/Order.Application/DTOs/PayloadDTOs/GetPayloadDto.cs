namespace Order.Application.DTOs.OrderDTOs;

public class GetPayloadDto
{
	public long Id { get; set; }
	
	public int Length { get; set; }
	public int Width { get; set; }
	public int Height { get; set; }
	public int Weight { get; set; }
	
	public string? Description { get; set; }
}
