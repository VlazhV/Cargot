using System.Threading.Tasks;
using AutoMapper;
using Autopark.Business.DTOs.CarDTOs;
using Autopark.Business.DTOs.SheduleDtos;
using Autopark.Business.Interfaces;
using Autopark.Business.Validators;
using Autopark.DataAccess.Entities;
using Autopark.DataAccess.Interfaces;
using FluentValidation;
using Identity.Business.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Autopark.Business.Services;

public class CarService : ICarService
{
	private readonly ICarRepository _carRepository;
	private readonly ICarInShipSheduleRepository _sheduleRepository;
	private readonly IMapper _mapper;
	
	public CarService
		(ICarRepository carRepository, 
		ICarInShipSheduleRepository sheduleRepository,
		IMapper mapper)
	{
		_carRepository = carRepository;
		_sheduleRepository = sheduleRepository;
		_mapper = mapper;
	}


	public async Task<GetCarDto> CreateAsync(UpdateCarDto carDto)
	{
		var car = _mapper.Map<Car>(carDto);
		car = await _carRepository.CreateAsync(car);

		return _mapper.Map<GetCarDto>(car);
	}

	public async Task DeleteAsync(int id)
	{
		var car = await _carRepository.GetByIdAsync(id)
			?? throw new ApiException("Car not found", ApiException.NotFound);

		await _carRepository.DeleteAsync(car);		
	}

	public async Task<IEnumerable<GetCarAutoparkDto>> GetAllAsync()
	{
		var cars = await _carRepository.GetAllAsync();
		
		return cars.Select(c => _mapper.Map<GetCarAutoparkDto>(c));
	}

	public async Task<GetCarAutoparkDto> GetByIdAsync(int id)
	{
		var car = await _carRepository.GetByIdAsync(id)
			?? throw new ApiException("Car not found", ApiException.NotFound);

		return _mapper.Map<GetCarAutoparkDto>(car);	
	}

	public async Task<GetCarDto> UpdateAsync(int id, UpdateCarDto carDto)
	{
		if (!_carRepository.DoesItExist(id))
			throw new ApiException("Car not found", ApiException.NotFound);
		
		var car = _mapper.Map<Car>(carDto);
		car.Id = id;

		car = await _carRepository.UpdateAsync(car);	
		
		return _mapper.Map<GetCarDto>(car);
	}
	
	public async Task<GetSheduleDto> AddPlannedSheduleAsync(int carId, UpdatePlanSheduleDto sheduleDto)
	{
		if (!_carRepository.DoesItExist(carId))
			throw new ApiException("Car not found", ApiException.NotFound);
		
		var shedule = _mapper.Map<CarInShipShedule>(sheduleDto);
		shedule.CarId = carId;

		shedule = await _sheduleRepository.CreateAsync(shedule);
		
		return _mapper.Map<GetSheduleDto>(shedule);
	}
	
	public async Task<GetSheduleDto> UpdatePlannedSheduleAsync(int sheduleId, UpdatePlanSheduleDto sheduleDto)
	{
		if (!_sheduleRepository.DoesItExist(sheduleId))
			throw new ApiException("Shedule not found", ApiException.NotFound);
		
		var shedule = _mapper.Map<CarInShipShedule>(sheduleDto);
		shedule.Id = sheduleId;
		
		shedule = await _sheduleRepository.UpdateAsync(shedule);	
		
		return _mapper.Map<GetSheduleDto>(shedule);
	}

	public async Task<GetSheduleDto> UpdateActualSheduleAsync(int sheduleId)
	{
		var shedule = await _sheduleRepository.GetByIdAsync(sheduleId)
			?? throw new ApiException("Shedule not found", ApiException.NotFound);

		if (shedule.Start is not null)
			shedule.Start = DateTime.UtcNow;		
		
		else if (shedule.Finish is not null)		
			shedule.Finish = DateTime.UtcNow;
		
		else
			throw new ApiException("Shedule is filled", ApiException.BadRequest);

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

	public async Task<IEnumerable<GetSheduleDto>> GetShedulesAsync(int carId)
	{
		if (!_carRepository.DoesItExist(carId))
			throw new ApiException("Car not found", ApiException.NotFound);
			
		var shedules = await _sheduleRepository.GetAllOfCarAsync(carId);

		return shedules.Select(s => _mapper.Map<GetSheduleDto>(s));
	}
}
