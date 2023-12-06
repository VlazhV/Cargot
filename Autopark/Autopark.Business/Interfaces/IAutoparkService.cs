using Autopark.Business.DTOs.AutoparkDTOs;

namespace Autopark.Business.Interfaces;

public interface IAutoparkService
{
	Task<GetAutoparkDto> CreateAsync(UpdateAutoparkDto autoparkDto);
	Task DeleteAsync(int id);
	Task<IEnumerable<GetAutoparkVehicleDto>> GetAllAsync();
	Task<GetAutoparkVehicleDto> GetByIdAsync(int id);
	Task<GetAutoparkDto> UpdateAsync(int id, UpdateAutoparkDto autoparkDto);
}
