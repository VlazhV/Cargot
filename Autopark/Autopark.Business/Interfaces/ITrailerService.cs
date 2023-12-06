using Autopark.Business.DTOs;
using Autopark.Business.DTOs.ScheduleDtos;
using Autopark.Business.DTOs.TrailerDTOs;

namespace Autopark.Business.Interfaces;

public interface ITrailerService
{
	Task<GetTrailerDto> CreateAsync(UpdateTrailerDto trailerDto);
	Task DeleteAsync(int id);
	Task<IEnumerable<GetTrailerAutoparkDto>> GetWithSpecsAsync(SpecDto specDto);
	Task<GetTrailerAutoparkDto> GetByIdAsync(int id);
	Task<GetTrailerDto> UpdateAsync(int id, UpdateTrailerDto trailerDto);
}
