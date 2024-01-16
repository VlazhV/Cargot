using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MongoDB.Bson;

namespace Ship.Domain.Entities;

public class Ship
{
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public ObjectId Id { get; set; }
	
	public DateTime PlannedStart { get; set; }
	public DateTime PlannedFinish { get; set; }
	
	public DateTime? Start { get; set; }
	public DateTime? Finish { get; set; }

	public long[] Orders { get; set; } = null!;
	
	public int AutoparkId { get; set; }
	public int CarId { get; set; }
	public int TrailerId { get; set; }
}
