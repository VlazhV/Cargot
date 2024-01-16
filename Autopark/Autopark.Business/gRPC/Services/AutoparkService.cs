using AutoMapper;
using Autopark.Business.DTOs;
using Autopark.Business.Interfaces;
using Autopark.gRPC.Requests;
using Autopark.gRPC.Responses;

namespace Autopark.gRPC.Services;

public class AutoparkService: IAutoparkService
{
	private readonly ICarService _carService;
	private readonly ITrailerService _trailerService;
	private readonly IMapper _mapper;
	
	public AutoparkService(ICarService carService, ITrailerService trailerService, IMapper mapper)
	{
		_carService = carService;
		_trailerService = trailerService;
		_mapper = mapper;
	}
	
	public async Task<IEnumerable<VehicleResponse>> GetFreeCarsAsync(TimeInterval timeInterval)
	{
		var specDto = _mapper.Map<SpecDto>(timeInterval);
		var carDtos = await _carService.GetWithSpecsAsync(specDto);

		return _mapper.Map<IEnumerable<VehicleResponse>>(carDtos);
	}

	public async Task<IEnumerable<VehicleResponse>> GetFreeTrailersAsync(TimeInterval timeInterval)
	{
		var specDto = _mapper.Map<SpecDto>(timeInterval);
		var trailerDtos = await _trailerService.GetWithSpecsAsync(specDto);

		return _mapper.Map<IEnumerable<VehicleResponse>>(trailerDtos);
	}

}