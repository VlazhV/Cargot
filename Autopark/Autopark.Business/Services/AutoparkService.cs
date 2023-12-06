using AutoMapper;
using Autopark.Business.DTOs.AutoparkDTOs;
using Autopark.Business.Interfaces;
using Autopark.DataAccess.Interfaces;
using Identity.Business.Exceptions;

namespace Autopark.Business.Services;

public class AutoparkService: IAutoparkService
{
	private readonly IAutoparkRepository _autoparkRepository;
	private readonly IMapper _mapper;
	
	public AutoparkService(IAutoparkRepository autoparkRepository, IMapper mapper)
	{
		_autoparkRepository = autoparkRepository;
		_mapper = mapper;
	}

	public async Task<GetAutoparkDto> CreateAsync(UpdateAutoparkDto autoparkDto)
	{
		var autopark = _mapper.Map<DataAccess.Entities.Autopark>(autoparkDto);
		
		autopark = await _autoparkRepository.CreateAsync(autopark);
		await _autoparkRepository.SaveChangesAsync();

		return _mapper.Map<GetAutoparkDto>(autopark);
	}

	public async Task DeleteAsync(int id)
	{
		var autopark = await _autoparkRepository.GetByIdAsync(id)
			?? throw new ApiException("Autopark not found", ApiException.NotFound);

		_autoparkRepository.Delete(autopark);
		await _autoparkRepository.SaveChangesAsync();
	}

	public async Task<IEnumerable<GetAutoparkVehicleDto>> GetAllAsync()
	{
		var autoparks = await _autoparkRepository.GetAllAsync();

		return autoparks.Select(autopark => _mapper.Map<GetAutoparkVehicleDto>(autopark));
	}

	public async Task<GetAutoparkVehicleDto> GetByIdAsync(int id)
	{
		var autopark = await _autoparkRepository.GetByIdAsync(id)
			?? throw new ApiException("Autopark not found", ApiException.NotFound);

		return _mapper.Map<GetAutoparkVehicleDto>(autopark);
	}

	public async Task<GetAutoparkDto> UpdateAsync(int id, UpdateAutoparkDto autoparkDto)
	{
		if (!_autoparkRepository.DoesItExist(id))
			throw new ApiException("Autopark not found", ApiException.NotFound);
		
		var autopark = _mapper.Map<DataAccess.Entities.Autopark>(autoparkDto);
		autopark.Id = id;
		
		autopark = _autoparkRepository.Update(autopark);
		await _autoparkRepository.SaveChangesAsync();
		
		return _mapper.Map<GetAutoparkDto>(autopark);
	}

}
