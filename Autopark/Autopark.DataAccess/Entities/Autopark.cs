namespace Autopark.DataAccess.Entities;

public class Autopark
{
	public int Id { get; set; }
	public string Address { get; set; } = null!;
	
	public List<Car>? Cars { get; set; }
	public List<Trailer>? Trailers { get; set; }
}
