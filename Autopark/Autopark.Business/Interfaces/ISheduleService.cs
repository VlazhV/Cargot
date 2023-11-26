using Autopark.Business.DTOs.ScheduleDtos;
using Autopark.Business.Services;

namespace Autopark.Business.Interfaces;

public interface IScheduleService
{
	Task<GetScheduleDto> GetScheduleByIdAsync(int id);
	Task<IEnumerable<GetScheduleDto>> GetSchedulesOfVehicleAsync(int vehicleId);
	Task<GetScheduleDto> AddPlannedScheduleAsync(int vehicleId, UpdatePlanScheduleDto scheduleDto);
	Task<GetScheduleDto> UpdatePlannedScheduleAsync(int id, UpdatePlanScheduleDto scheduleDto);
	Task<GetScheduleDto> UpdateActualScheduleAsync(int id);
	Task DeleteScheduleAsync(int id);	
}
