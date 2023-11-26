namespace Autopark.DataAccess.Entities;

public interface ISchedulable<T> where T: ISchedule
{
	List<T> Schedules { get; set; }
}
