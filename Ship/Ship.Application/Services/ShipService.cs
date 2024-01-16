using AutoMapper;
using Ship.Application.Constants;
using Ship.Application.DTOs;
using Ship.Application.Exceptions;
using Ship.Application.Interfaces;
using Ship.Domain.Interfaces;
using MongoDB.Bson;
using Ship.Application.Specifications;
using Autopark.gRPC.Responses;
using Autopark.gRPC.Services;
using Autopark.gRPC.Requests;

namespace Ship.Application.Services;

public class ShipService: IShipService
{
	private readonly IShipRepository _shipRepository;
	private readonly IAutoparkService _autoparkService;
	private readonly IMapper _mapper;
	
	public ShipService
		(IShipRepository shipRepository, 
		IMapper mapper,
		IAutoparkService autoparkService)
	{
		_shipRepository = shipRepository;
		_mapper = mapper;
		_autoparkService = autoparkService;
	}

	public async Task<GetShipDto> CreateAsync(UpdateShipDto shipDto, CancellationToken cancellationToken)
	{
		var ship = _mapper.Map<Domain.Entities.Ship>(shipDto);
		ship.Id = ObjectId.GenerateNewId();

		ship = await _shipRepository.CreateAsync(ship, cancellationToken);
		cancellationToken.ThrowIfCancellationRequested();
		await _shipRepository.SaveChangesAsync(cancellationToken);

		return _mapper.Map<GetShipDto>(ship);
	}

	public async Task DeleteAsync(string id, CancellationToken cancellationToken)
	{
		var objectId = ObjectId.Parse(id);
		var ship = await _shipRepository.GetByIdAsync(objectId, cancellationToken);
		
		if (ship is null)
		{
			throw new ApiException(Messages.ShipIsNotFound, ApiException.NotFound);
		}

		_shipRepository.Delete(ship);
		cancellationToken.ThrowIfCancellationRequested();
		await _shipRepository.SaveChangesAsync(cancellationToken);				
	}

	public async Task<IEnumerable<GetShipDto>> GetAllAsync(PagingDto pagingDto, CancellationToken cancellationToken)
	{		
		var pageSpec = new PageSpecification<Domain.Entities.Ship>(pagingDto.Offset, pagingDto.Limit);
		var specs = new ISpecification<Domain.Entities.Ship>[]
		{
			pageSpec
		};

		var ships = await _shipRepository.GetAllAsync(specs, cancellationToken);

		return _mapper.Map<IEnumerable<GetShipDto>>(ships);
	}

	public async Task<GetShipDto> GetByIdAsync(string id, CancellationToken cancellationToken)
	{
		var objectId = ObjectId.Parse(id);
		var ship = await _shipRepository.GetByIdAsync(objectId, cancellationToken);
		
		if (ship is null)
		{
			throw new ApiException(Messages.ShipIsNotFound, ApiException.NotFound);
		}

		return _mapper.Map<GetShipDto>(ship);
	}

	public async Task<GetShipDto> MarkAsync(string id, CancellationToken cancellationToken)
	{
		var objectId = ObjectId.Parse(id);
		var ship = await _shipRepository.GetByIdAsync(objectId, cancellationToken);
		
		if (ship is null)
		{
			throw new ApiException(Messages.ShipIsNotFound, ApiException.NotFound);
		}

		switch (ship.Start)
		{
			case null:
				ship.Start = DateTime.UtcNow;
				break;
			
			case not null when ship.Finish is null:
				ship.Finish = DateTime.UtcNow;
				break;
				
			default:
				throw new ApiException(Messages.ShipIsFullMarked, ApiException.BadRequest);
		}

		_shipRepository.Update(ship);
		cancellationToken.ThrowIfCancellationRequested();
		await _shipRepository.SaveChangesAsync(cancellationToken);

		return _mapper.Map<GetShipDto>(ship);
	}

	public async Task<GetShipDto> UpdateAsync(string id, UpdateShipDto shipDto, CancellationToken cancellationToken)
	{
		var objectId = ObjectId.Parse(id);
		
		if (!await _shipRepository.IsShipExists(objectId, cancellationToken))
		{
			throw new ApiException(Messages.ShipIsNotFound, ApiException.NotFound);
		}
		
		var ship = _mapper.Map<Domain.Entities.Ship>(shipDto);
		ship.Id = new ObjectId(id.ToString());

		_shipRepository.Update(ship);
		cancellationToken.ThrowIfCancellationRequested();
		await _shipRepository.SaveChangesAsync(cancellationToken);

		return _mapper.Map<GetShipDto>(ship);
	}
	
	public async Task<GetShipDto> GenerateShipAsync(GenerateShipDto shipDto, CancellationToken cancellationToken)
	{
		var interval = _mapper.Map<TimeInterval>(shipDto);

		var isSameAutopark = true;

		var cars = await _autoparkService.GetFreeCarsAsync(interval);
		var trailers = await _autoparkService.GetFreeTrailersAsync(interval);
		
		if (!cars.Any() || !trailers.Any())
		{
			throw new ApiException(Messages.ThereIsNoFreeCarOrTrailer, ApiException.NotAcceptable);
		}

		var minAutoparkId = Math.Max(
			cars.Min(car => car.AutoparkId), 
			trailers.Min(trailer => trailer.AutoparkId)
		);

		var maxAutoparkId = Math.Min(
			cars.Max(car => car.AutoparkId),
			trailers.Max(trailer => trailer.AutoparkId)
		);

		IEnumerable<VehicleResponse> suitCars = new VehicleResponse[] { };
		IEnumerable<VehicleResponse> suitTrailers = new VehicleResponse[] { };
		
		
		for (int autoparkId = minAutoparkId; autoparkId <= maxAutoparkId; autoparkId++)
		{
			suitCars = cars.Where(car => car.AutoparkId == autoparkId);
			suitTrailers = trailers.Where(trailer => trailer.AutoparkId == autoparkId);			
			
			if (!suitCars.Any() && !suitTrailers.Any())
			{
				if (autoparkId == maxAutoparkId)
					isSameAutopark = false;
			} 
			else 
			{
				break;
			} 
		}

		var ship = _mapper.Map<Domain.Entities.Ship>(shipDto);

		ship.Id = ObjectId.GenerateNewId();
		
		if (isSameAutopark)
		{
			ship.AutoparkId = suitCars.First().AutoparkId;
			ship.CarId = suitCars.First().Id;
			ship.TrailerId = suitTrailers.First().Id;
		} 
		else
		{
			ship.AutoparkId = cars.First().AutoparkId;
			ship.CarId = cars.First().Id;
			ship.TrailerId = trailers.First().Id;
		}

		ship = await _shipRepository.CreateAsync(ship, cancellationToken);
		await _shipRepository.SaveChangesAsync(cancellationToken);

		return _mapper.Map<GetShipDto>(ship);
	}
}
