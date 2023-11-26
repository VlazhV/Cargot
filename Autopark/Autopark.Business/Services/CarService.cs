using AutoMapper;
using Autopark.Business.DTOs;
using Autopark.Business.DTOs.CarDTOs;
using Autopark.Business.Interfaces;
using Autopark.DataAccess.Entities;
using Autopark.DataAccess.Interfaces;
using Autopark.DataAccess.Repositories;
using Autopark.DataAccess.Specifications;
using Identity.Business.Exceptions;

namespace Autopark.Business.Services;

public class CarService : ICarService
{
	private readonly CarRepository _carRepository;
	private readonly CarInShipScheduleRepository _scheduleRepository;
	private readonly IMapper _mapper;
	
	public CarService
		(CarRepository carRepository, 
		CarInShipScheduleRepository scheduleRepository,
		IMapper mapper)
	{
		_carRepository = carRepository;
		_scheduleRepository = scheduleRepository;
		_mapper = mapper;
	}


	public async Task<GetCarDto> CreateAsync(UpdateCarDto carDto)
	{
		if (_carRepository.DoesItExist(carDto.LicenseNumber!))
		{
			throw new ApiException("license number is reserved", ApiException.BadRequest);
		}
						
		var car = _mapper.Map<Car>(carDto);
		
		car = await _carRepository.CreateAsync(car);
		await _carRepository.SaveChangesAsync();

		return _mapper.Map<GetCarDto>(car);
	}

	public async Task DeleteAsync(int id)
	{
		var car = await _carRepository.GetByIdAsync(id)
			?? throw new ApiException("Car not found", ApiException.NotFound);

		_carRepository.Delete(car);
		await _carRepository.SaveChangesAsync();
	}

	public async Task<IEnumerable<GetCarAutoparkDto>> GetWithSpecsAsync(SpecDto specDto)
	{
		List<ISpecification<Car>> specs = new();

		ISchedulable<CarInShipSchedule> x = new Car();
		
		if (specDto.FreeOnly)
		{
			var freeOnlyCarSpec = new FreeOnlySpecification<Car, CarInShipSchedule>
				(specDto.Start.GetValueOrDefault(), specDto.Finish.GetValueOrDefault());
				
			specs.Add(freeOnlyCarSpec);
		}
		
		var cars = await _carRepository.GetWithSpecsAsync(specs);
		
		return cars.Select(car => _mapper.Map<GetCarAutoparkDto>(car));
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
		{
			throw new ApiException("Car not found", ApiException.NotFound);
		}
			
		if (_carRepository.DoesItExist(carDto.LicenseNumber!))
		{
			throw new ApiException("License number is reserved", ApiException.BadRequest);
		}			
		
		var car = _mapper.Map<Car>(carDto);
		car.Id = id;

		car = _carRepository.Update(car);
		await _carRepository.SaveChangesAsync();
		
		return _mapper.Map<GetCarDto>(car);
	}	
}
