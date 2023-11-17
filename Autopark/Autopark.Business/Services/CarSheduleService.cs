using AutoMapper;
using Autopark.Business.DTOs.SheduleDtos;
using Autopark.Business.Interfaces;
using Autopark.DataAccess.Entities;
using Autopark.DataAccess.Interfaces;
using Identity.Business.Exceptions;

namespace Autopark.Business.Services;

public class CarSheduleService: ICarSheduleService
{
	private readonly ICarInShipSheduleRepository _sheduleRepository;
	private readonly ICarRepository _carRepository;
	private readonly IMapper _mapper;
	
	public CarSheduleService
		(ICarInShipSheduleRepository carInShipSheduleRepository,		
		ICarRepository carRepository,
		IMapper mapper)
	{		
		_sheduleRepository = carInShipSheduleRepository;
		_carRepository = carRepository;		
		_mapper = mapper;
	}

	public async Task<GetSheduleDto> AddPlannedSheduleAsync(int vehicleId, UpdatePlanSheduleDto sheduleDto)
	{
		if (!_carRepository.DoesItExist(vehicleId)){
			throw new ApiException("Car not found", ApiException.NotFound);
		}
		
		var shedule = _mapper.Map<CarInShipShedule>(sheduleDto);
		shedule.CarId = vehicleId;

		shedule = await _sheduleRepository.CreateAsync(shedule);
		
		return _mapper.Map<GetSheduleDto>(shedule);
	}
	
	public async Task<GetSheduleDto> UpdatePlannedSheduleAsync(int sheduleId, UpdatePlanSheduleDto sheduleDto)
	{
		var shedule = await _sheduleRepository.GetByIdAsync(sheduleId)
			?? throw new ApiException("Shedule not found", ApiException.NotFound);

		shedule.PlanStart = sheduleDto.PlanStart;
		shedule.PlanFinish = sheduleDto.PlanFinish;
				
		shedule = await _sheduleRepository.UpdateAsync(shedule);	
		
		return _mapper.Map<GetSheduleDto>(shedule);
	}

	public async Task<GetSheduleDto> UpdateActualSheduleAsync(int sheduleId)
	{
		var shedule = await _sheduleRepository.GetByIdAsync(sheduleId)
			?? throw new ApiException("Shedule not found", ApiException.NotFound);

		if (shedule.Start is null)
		{
			shedule.Start = DateTime.UtcNow;		
		}					
		else if (shedule.Finish is null)
		{
			shedule.Finish = DateTime.UtcNow;
		}					
		else
		{
			throw new ApiException("Shedule is filled", ApiException.BadRequest);
		}
			

		shedule = await _sheduleRepository.UpdateAsync(shedule);

		return _mapper.Map<GetSheduleDto>(shedule);
	}
	
	public async Task DeleteSheduleAsync(int sheduleId)
	{
		var shedule = await _sheduleRepository.GetByIdAsync(sheduleId)
			?? throw new ApiException("Shedule not found", ApiException.NotFound);

		await _sheduleRepository.DeleteAsync(shedule);				
	}

	public async Task<GetSheduleDto> GetSheduleByIdAsync(int sheduleId)
	{
		var shedule = await _sheduleRepository.GetByIdAsync(sheduleId)
			?? throw new ApiException("Shedule not found", ApiException.NotFound);

		return _mapper.Map<GetSheduleDto>(shedule);		
	}

	public async Task<IEnumerable<GetSheduleDto>> GetShedulesOfVehicleAsync(int vehicleId)
	{
		if (!_carRepository.DoesItExist(vehicleId))
			throw new ApiException("Car not found", ApiException.NotFound);
			
		var shedules = await _sheduleRepository.GetAllOfCarAsync(vehicleId);

		return shedules.Select(s => _mapper.Map<GetSheduleDto>(s));
	}
}
