namespace Order.Application.DTOs.PayloadDTOs;

public class GetPayloadDto
{
	public long Id { get; set; }
	
	public int Length { get; set; }
	public int Width { get; set; }
	public int Height { get; set; }
	public int Weight { get; set; }
	
	public string? Description { get; set; }
}
