using Autopark.Business.DTOs.SheduleDtos;
using Autopark.Business.DTOs.TrailerDTOs;

namespace Autopark.Business.Interfaces;

public interface ITrailerService
{
	Task<GetTrailerDto> CreateAsync(UpdateTrailerDto trailerDto);
	Task DeleteAsync(int id);
	Task<IEnumerable<GetTrailerAutoparkDto>> GetAllAsync();
	Task<GetTrailerAutoparkDto> GetByIdAsync(int id);
	Task<GetTrailerDto> UpdateAsync(int id, UpdateTrailerDto trailerDto);

	Task<GetSheduleDto> AddPlannedSheduleAsync(int trailerId, UpdatePlanSheduleDto sheduleDto);
	Task<GetSheduleDto> UpdatePlannedSheduleAsync(int sheduleId, UpdatePlanSheduleDto sheduleDto);
	Task<GetSheduleDto> UpdateActualSheduleAsync(int sheduleId);
	Task DeleteSheduleAsync(int sheduleId);
}
