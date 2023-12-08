namespace Ship.Domain.Entities;

public class Driver
{
	public int Id { get; set; }

	public string FirstName { get; set; } = null!;
	public string SecondName { get; set; } = null!;
	public string? MiddleName { get; set; }
	
	public IEnumerable<Ship>? Ships { get; set; }
}
