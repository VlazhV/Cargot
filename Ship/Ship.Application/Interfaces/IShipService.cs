using Ship.Application.DTOs;

namespace Ship.Application.Interfaces;

public interface IShipService
{
	Task<GetShipDto> GetByIdAsync(string id, CancellationToken cancellationToken);
	Task<IEnumerable<GetShipDto>> GetAllAsync(PagingDto pagingDto, CancellationToken cancellationToken);
	Task<GetShipDto> CreateAsync(UpdateShipDto shipDto, CancellationToken cancellationToken);
	Task<GetShipDto> UpdateAsync(string id, UpdateShipDto shipDto, CancellationToken cancellationToken);
	Task DeleteAsync(string id, CancellationToken cancellationToken);

	Task<GetShipDto> MarkAsync(string id, CancellationToken cancellationToken);
}
