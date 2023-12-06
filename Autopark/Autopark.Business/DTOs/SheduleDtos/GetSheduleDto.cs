namespace Autopark.Business.DTOs.ScheduleDtos;

public class GetScheduleDto
{
	public int Id { get; set; }
	
	public DateTime? Start { get; set; }
	public DateTime? Finish { get; set; }
	
	public DateTime PlanStart { get; set; }
	public DateTime PlanFinish { get; set; }
}
