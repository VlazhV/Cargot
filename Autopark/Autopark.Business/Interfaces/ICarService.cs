using Autopark.Business.DTOs.CarDTOs;
using Autopark.Business.DTOs.SheduleDtos;

namespace Autopark.Business.Interfaces;

public interface ICarService
{
	Task<GetCarDto> CreateAsync(UpdateCarDto carDto);
	Task DeleteAsync(int id);
	Task<IEnumerable<GetCarAutoparkDto>> GetAllAsync();
	Task<GetCarAutoparkDto> GetByIdAsync(int id);
	Task<GetCarDto> UpdateAsync(int id, UpdateCarDto carDto);

	Task<GetSheduleDto> GetSheduleByIdAsync(int sheduleId);
	Task<IEnumerable<GetSheduleDto>> GetShedulesAsync(int carId);
	Task<GetSheduleDto> AddPlannedSheduleAsync(int carId, UpdatePlanSheduleDto sheduleDto);
	Task<GetSheduleDto> UpdatePlannedSheduleAsync(int sheduleId, UpdatePlanSheduleDto sheduleDto);
	Task<GetSheduleDto> UpdateActualSheduleAsync(int sheduleId);
	Task DeleteSheduleAsync(int sheduleId);	
}