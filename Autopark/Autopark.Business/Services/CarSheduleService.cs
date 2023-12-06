using AutoMapper;
using Autopark.Business.DTOs.ScheduleDtos;
using Autopark.Business.Interfaces;
using Autopark.DataAccess.Entities;
using Autopark.DataAccess.Interfaces;
using Identity.Business.Exceptions;

namespace Autopark.Business.Services;

public class CarScheduleService: ICarScheduleService
{
	private readonly ICarInShipScheduleRepository _scheduleRepository;
	private readonly ICarRepository _carRepository;
	private readonly IMapper _mapper;
	
	public CarScheduleService
		(ICarInShipScheduleRepository carInShipScheduleRepository,		
		ICarRepository carRepository,
		IMapper mapper)
	{		
		_scheduleRepository = carInShipScheduleRepository;
		_carRepository = carRepository;		
		_mapper = mapper;
	}

	public async Task<GetScheduleDto> AddPlannedScheduleAsync(int vehicleId, UpdatePlanScheduleDto scheduleDto)
	{
		if (!_carRepository.DoesItExist(vehicleId)){
			throw new ApiException("Car not found", ApiException.NotFound);
		}
		
		var schedule = _mapper.Map<CarInShipSchedule>(scheduleDto);
		schedule.CarId = vehicleId;

		schedule = await _scheduleRepository.CreateAsync(schedule);
		await _scheduleRepository.SaveChangesAsync();
		
		return _mapper.Map<GetScheduleDto>(schedule);
	}
	
	public async Task<GetScheduleDto> UpdatePlannedScheduleAsync(int scheduleId, UpdatePlanScheduleDto scheduleDto)
	{
		var schedule = await _scheduleRepository.GetByIdAsync(scheduleId)
			?? throw new ApiException("Schedule not found", ApiException.NotFound);

		schedule.PlanStart = scheduleDto.PlanStart;
		schedule.PlanFinish = scheduleDto.PlanFinish;
				
		schedule = _scheduleRepository.Update(schedule);
		await _scheduleRepository.SaveChangesAsync();
		
		return _mapper.Map<GetScheduleDto>(schedule);
	}

	public async Task<GetScheduleDto> UpdateActualScheduleAsync(int scheduleId)
	{
		var schedule = await _scheduleRepository.GetByIdAsync(scheduleId)
			?? throw new ApiException("Schedule not found", ApiException.NotFound);

		switch (schedule.Start)
		{
			case null:
				schedule.Start = DateTime.UtcNow;
				break;
			case not null when schedule.Finish is null:
				schedule.Finish = DateTime.UtcNow;
				break;
			default:
				throw new ApiException("Schedule is filled", ApiException.BadRequest);
		}
			

		schedule = _scheduleRepository.Update(schedule);
		await _scheduleRepository.SaveChangesAsync();

		return _mapper.Map<GetScheduleDto>(schedule);
	}
	
	public async Task DeleteScheduleAsync(int scheduleId)
	{
		var schedule = await _scheduleRepository.GetByIdAsync(scheduleId)
			?? throw new ApiException("Schedule not found", ApiException.NotFound);

		_scheduleRepository.Delete(schedule);
		await _scheduleRepository.SaveChangesAsync();
	}

	public async Task<GetScheduleDto> GetScheduleByIdAsync(int scheduleId)
	{
		var schedule = await _scheduleRepository.GetByIdAsync(scheduleId)
			?? throw new ApiException("Schedule not found", ApiException.NotFound);

		return _mapper.Map<GetScheduleDto>(schedule);		
	}

	public async Task<IEnumerable<GetScheduleDto>> GetSchedulesOfVehicleAsync(int vehicleId)
	{
		if (!_carRepository.DoesItExist(vehicleId))
			throw new ApiException("Car not found", ApiException.NotFound);
			
		var schedules = await _scheduleRepository.GetAllOfCarAsync(vehicleId);

		return schedules.Select(schedule => _mapper.Map<GetScheduleDto>(schedule));
	}
}
