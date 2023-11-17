using System.Threading.Tasks;
using AutoMapper;
using Autopark.Business.DTOs;
using Autopark.Business.DTOs.CarDTOs;
using Autopark.Business.DTOs.SheduleDtos;
using Autopark.Business.Interfaces;
using Autopark.Business.Validators;
using Autopark.DataAccess.Entities;
using Autopark.DataAccess.Interfaces;
using Autopark.DataAccess.Specifications;
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
		if (_carRepository.DoesItExist(carDto.LicenseNumber!))
		{
			throw new ApiException("license number is reserved", ApiException.BadRequest);
		}
			
			
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

	public async Task<IEnumerable<GetCarAutoparkDto>> GetWithSpecsAsync(SpecDto specDto)
	{
		List<ISpecification<Car>> specs = new();
		if (specDto.FreeOnly)
		{
			specs.Add(
				new FreeOnlyCarSpecification
					(specDto.Start.GetValueOrDefault(), specDto.Finish.GetValueOrDefault())
			);
		}
		
		var cars = await _carRepository.GetWithSpecsAsync(specs);
		
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
		{
			throw new ApiException("Car not found", ApiException.NotFound);
		}
			
		if (_carRepository.DoesItExist(carDto.LicenseNumber!))
		{
			throw new ApiException("License number is reserved", ApiException.BadRequest);
		}			
		
		var car = _mapper.Map<Car>(carDto);
		car.Id = id;

		car = await _carRepository.UpdateAsync(car);	
		
		return _mapper.Map<GetCarDto>(car);
	}	
}
