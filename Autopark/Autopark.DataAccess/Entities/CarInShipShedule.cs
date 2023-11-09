namespace Autopark.DataAccess.Entities;

public class CarInShipShedule
{
	public int Id { get; set; }
	
	public DateTime? Start { get; set; }
	public DateTime? Finish { get; set; }
	
	public DateTime PlanStart { get; set; }
	public DateTime PlanFinish { get; set; }


	public Car Car { get; set; } = null!;
	public int CarId { get; set; }
}
