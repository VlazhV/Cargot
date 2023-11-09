namespace Autopark.DataAccess.Entities;

public class TrailerInShipShedule
{
	public int Id { get; set; }
	
	public DateTime? Start { get; set; }
	public DateTime? Finish { get; set; }
	
	public DateTime PlanStart { get; set; }
	public DateTime PlanFinish { get; set; }


	public Trailer Trailer { get; set; } = null!;
	public int TrailerId { get; set; }
}
