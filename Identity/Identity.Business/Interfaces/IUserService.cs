using Identity.Business.DTOs;

namespace Identity.Business.Interfaces;

public interface IUserService
{
	Task<TokenDto> LoginAsync(LoginDto loginModel);
	Task<TokenDto> SignUpAsync(SignupDto signupModel);
	Task<UserDto> RegisterAsync(RegisterDto registerModel, string? registererRole);
	
	Task<IEnumerable<UserIdDto>> GetAllWithSpecAsync(SpecDto specDto, string? userRole);
	Task<UserDto> GetByIdAsync(string? id, string? userRole);

	Task<UserDto> UpdateAsync(string? id, UserUpdateDto userUpdateDto);
	Task UpdatePasswordAsync(string? id, PasswordDto passwordDto);

	Task DeleteAsync(string? id, string? userRole);
}
