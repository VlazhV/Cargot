using Order.Application.DTOs.UserDTOs;

namespace Order.Application.Interfaces;

public interface IUserService
{
	Task<GetUserInfoDto> GetByIdAsync(long id);
	Task<IEnumerable<GetUserDto>> GetAllAsync();
	Task<GetUserDto> CreateAsync(UpdateUserDto userDto);
	Task<GetUserDto> UpdateAsync(long id, UpdateUserDto userDto);
	Task DeleteAsync(long id);
}
