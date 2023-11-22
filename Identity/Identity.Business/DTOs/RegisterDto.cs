namespace Identity.Business.DTOs;

public class RegisterDto
{	
	public string? UserName { get; set; }
	public string? Email { get; set; }
	public string? PhoneNumber { get; set; }
	public string? Role { get; set; }
}