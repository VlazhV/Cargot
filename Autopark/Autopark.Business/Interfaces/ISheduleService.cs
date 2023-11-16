using Autopark.Business.DTOs.SheduleDtos;
using Autopark.Business.Services;

namespace Autopark.Business.Interfaces;

public interface ISheduleService
{
	Task<GetSheduleDto> GetSheduleByIdAsync(int id);
	Task<IEnumerable<GetSheduleDto>> GetShedulesOfVehicleAsync(int vehicleId);
	Task<GetSheduleDto> AddPlannedSheduleAsync(int vehicleId, UpdatePlanSheduleDto sheduleDto);
	Task<GetSheduleDto> UpdatePlannedSheduleAsync(int id, UpdatePlanSheduleDto sheduleDto);
	Task<GetSheduleDto> UpdateActualSheduleAsync(int id);
	Task DeleteSheduleAsync(int id);	
}
