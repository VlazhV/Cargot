using AutoMapper;
using Autopark.Business.DTOs.SheduleDtos;
using Autopark.Business.DTOs.TrailerDTOs;
using Autopark.Business.Interfaces;
using Autopark.Business.Validators;
using Autopark.DataAccess.Entities;
using Autopark.DataAccess.Interfaces;
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

	public async Task<IEnumerable<GetTrailerAutoparkDto>> GetAllAsync()
	{
		var trailers = await _trailerRepository.GetAllAsync();

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
		
		var trailer = _mapper.Map<Trailer>(trailerDto);
		trailer.Id = id;

		trailer = await _trailerRepository.UpdateAsync(trailer);	
		
		return _mapper.Map<GetTrailerDto>(trailer);
	}


	public async Task<GetSheduleDto> AddPlannedSheduleAsync(int trailerId, UpdatePlanSheduleDto sheduleDto)
	{
		var shedule = _mapper.Map<TrailerInShipShedule>(sheduleDto);
		shedule.Id = trailerId;

		shedule = await _sheduleRepository.CreateAsync(shedule);
		
		return _mapper.Map<GetSheduleDto>(shedule);
	}
	
	public async Task<GetSheduleDto> UpdatePlannedSheduleAsync(int sheduleId, UpdatePlanSheduleDto sheduleDto)
	{
		if (!_sheduleRepository.DoesItExist(sheduleId))
			throw new ApiException("Shedule not found", ApiException.NotFound);
			
		var shedule = _mapper.Map<TrailerInShipShedule>(sheduleDto);
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
}
