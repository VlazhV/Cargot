namespace Order.Application.DTOs.UserDTOs;

public class UserMessageDto
{
	public long Id { get; set; }
	public string? UserName { get; set; }	
	public string? Email { get; set; }
	public string? PhoneNumber { get; set; }  
}