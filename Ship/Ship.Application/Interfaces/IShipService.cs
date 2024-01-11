using Ship.Application.DTOs;

namespace Ship.Application.Interfaces;

public interface IShipService
{
	Task<GetShipDto> GetByIdAsync(long id);
	Task<IEnumerable<GetShipDto>> GetAllAsync();
	Task<GetShipDto> CreateAsync(UpdateShipDto shipDto);
	Task<GetShipDto> UpdateAsync(long id, UpdateShipDto shipDto);
	Task DeleteAsync(long id);

    Task<GetShipDto> MarkAsync(long id);
}
