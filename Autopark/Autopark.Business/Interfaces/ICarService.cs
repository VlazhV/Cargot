using Autopark.Business.DTOs;
using Autopark.Business.DTOs.CarDTOs;
using Autopark.Business.DTOs.ScheduleDtos;

namespace Autopark.Business.Interfaces;

public interface ICarService
{
	Task<GetCarDto> CreateAsync(UpdateCarDto carDto);
	Task DeleteAsync(int id);
	Task<IEnumerable<GetCarAutoparkDto>> GetWithSpecsAsync(SpecDto specDto);
	Task<GetCarAutoparkDto> GetByIdAsync(int id);
	Task<GetCarDto> UpdateAsync(int id, UpdateCarDto carDto);	
}