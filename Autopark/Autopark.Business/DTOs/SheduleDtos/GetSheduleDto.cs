namespace Autopark.Business.DTOs.SheduleDtos;

public class GetSheduleDto
{
	public int Id { get; set; }
	
	public DateTime? Start { get; set; }
	public DateTime? Finish { get; set; }
	
	public DateTime PlanStart { get; set; }
	public DateTime PlanFinish { get; set; }
}
