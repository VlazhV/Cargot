using Ship.Application.DTOs;

namespace Ship.Application.Interfaces;

public interface IShipService
{
	Task<GetShipDto> GetByIdAsync(string id);
	Task<IEnumerable<GetShipDto>> GetAllAsync();
	Task<GetShipDto> CreateAsync(UpdateShipDto shipDto);
	Task<GetShipDto> UpdateAsync(string id, UpdateShipDto shipDto);
	Task DeleteAsync(string id);

    Task<GetShipDto> MarkAsync(string id);
}
