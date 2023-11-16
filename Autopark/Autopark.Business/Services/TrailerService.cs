using AutoMapper;
using Autopark.Business.DTOs;
using Autopark.Business.DTOs.SheduleDtos;
using Autopark.Business.DTOs.TrailerDTOs;
using Autopark.Business.Interfaces;
using Autopark.Business.Validators;
using Autopark.DataAccess.Entities;
using Autopark.DataAccess.Interfaces;
using Autopark.DataAccess.Specifications;
using FluentValidation;
using Identity.Business.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Autopark.Business.Services;

public class TrailerService: ITrailerService
{
	private readonly ITrailerRepository _trailerRepository;
	private readonly ITrailerInShipSheduleRepository _sheduleRepository;
	private readonly IMapper _mapper;
	
	public TrailerService
		(ITrailerRepository trailerRepository, 
		ITrailerInShipSheduleRepository sheduleRepository,
		IMapper mapper)
	{
		_trailerRepository = trailerRepository;
		_sheduleRepository = sheduleRepository;
		_mapper = mapper;
	}

	

	public async Task<GetTrailerDto> CreateAsync(UpdateTrailerDto trailerDto)
	{
		if (_trailerRepository.DoesItExist(trailerDto.LicenseNumber!))
			throw new ApiException("License number is reserved", ApiException.BadRequest);
			
		var trailer = _mapper.Map<Trailer>(trailerDto);
		trailer = await _trailerRepository.CreateAsync(trailer);

		return _mapper.Map<GetTrailerDto>(trailer);
	}

	public async Task DeleteAsync(int id)
	{
		var trailer = await _trailerRepository.GetByIdAsync(id)
			?? throw new ApiException("Trailer not found", ApiException.NotFound);

		await _trailerRepository.DeleteAsync(trailer);
	}

	public async Task<IEnumerable<GetTrailerAutoparkDto>> GetWithSpecsAsync(SpecDto specDto)
	{
		var specs = new List<ISpecification<Trailer>>();
		if (specDto.FreeOnly)
		{
			specs.Add(
				new FreeOnlyTrailerSpecification
					(specDto.Start.GetValueOrDefault(), specDto.Finish.GetValueOrDefault())
			);
		}		
		
		var trailers = await _trailerRepository.GetWithSpecsAsync(specs);

		return trailers.Select(t => _mapper.Map<GetTrailerAutoparkDto>(t));
	}

	public async Task<GetTrailerAutoparkDto> GetByIdAsync(int id)
	{
		var trailer = await _trailerRepository.GetByIdAsync(id)
			?? throw new ApiException("Trailer not found", ApiException.NotFound);

		return _mapper.Map<GetTrailerAutoparkDto>(trailer);
	}

	public async Task<GetTrailerDto> UpdateAsync(int id, UpdateTrailerDto trailerDto)
	{
		if (!_trailerRepository.DoesItExist(id))
			throw new ApiException("Trailer not found", ApiException.NotFound);
			
		if (_trailerRepository.DoesItExist(trailerDto.LicenseNumber!))
			throw new ApiException("License number is reserved", ApiException.BadRequest);
		
		var trailer = _mapper.Map<Trailer>(trailerDto);
		trailer.Id = id;

		trailer = await _trailerRepository.UpdateAsync(trailer);	
		
		return _mapper.Map<GetTrailerDto>(trailer);
	}
}
