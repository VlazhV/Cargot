using AutoMapper;
using Autopark.Business.DTOs;
using Autopark.Business.DTOs.TrailerDTOs;
using Autopark.Business.Interfaces;
using Autopark.DataAccess.Entities;
using Autopark.DataAccess.Interfaces;
using Autopark.DataAccess.Specifications;
using Identity.Business.Exceptions;

namespace Autopark.Business.Services;

public class TrailerService: ITrailerService
{
	private readonly ITrailerRepository _trailerRepository;
	private readonly IMapper _mapper;
	
	public TrailerService
		(ITrailerRepository trailerRepository,
		IMapper mapper)
	{
		_trailerRepository = trailerRepository;
		_mapper = mapper;
	}

	

	public async Task<GetTrailerDto> CreateAsync(UpdateTrailerDto trailerDto)
	{
		if (await _trailerRepository.DoesItExistAsync(trailerDto.LicenseNumber!))
		{
			throw new ApiException("License number is reserved", ApiException.BadRequest);
		}			
			
		var trailer = _mapper.Map<Trailer>(trailerDto);
		trailer = await _trailerRepository.CreateAsync(trailer);
		await _trailerRepository.SaveChangesAsync();

		return _mapper.Map<GetTrailerDto>(trailer);
	}

	public async Task DeleteAsync(int id)
	{
		var trailer = await _trailerRepository.GetByIdAsync(id)
			?? throw new ApiException("Trailer not found", ApiException.NotFound);

		_trailerRepository.Delete(trailer);
		await _trailerRepository.SaveChangesAsync();
	}

	public async Task<IEnumerable<GetTrailerAutoparkDto>> GetWithSpecsAsync(SpecDto specDto)
	{
		var specs = new List<ISpecification<Trailer>>();
		
		if (specDto.FreeOnly)
		{
			var freeOnlySpec = new FreeOnlySpecification<Trailer, TrailerInShipSchedule>
					(specDto.Start.GetValueOrDefault(), specDto.Finish.GetValueOrDefault());
			
			specs.Add(freeOnlySpec);
		}		
		
		var trailers = await _trailerRepository.GetWithSpecsAsync(specs);

		return trailers.Select(trailer => _mapper.Map<GetTrailerAutoparkDto>(trailer));
	}

	public async Task<GetTrailerAutoparkDto> GetByIdAsync(int id)
	{
		var trailer = await _trailerRepository.GetByIdAsync(id)
			?? throw new ApiException("Trailer not found", ApiException.NotFound);

		return _mapper.Map<GetTrailerAutoparkDto>(trailer);
	}

	public async Task<GetTrailerDto> UpdateAsync(int id, UpdateTrailerDto trailerDto)
	{
		if (! await _trailerRepository.DoesItExistAsync(id))
		{
			throw new ApiException("Trailer not found", ApiException.NotFound);
		}
						
		if (await _trailerRepository.DoesItExistAsync(trailerDto.LicenseNumber!))
		{
			throw new ApiException("License number is reserved", ApiException.BadRequest);	
		}			
		
		var trailer = _mapper.Map<Trailer>(trailerDto);
		trailer.Id = id;

		trailer = _trailerRepository.Update(trailer);
		await _trailerRepository.SaveChangesAsync();
		
		return _mapper.Map<GetTrailerDto>(trailer);
	}
}
