using Identity.Business.DTOs;

namespace Identity.Business.Interfaces;

public interface IUserService
{
	Task<TokenDto> Login(LoginDto loginModel);
	Task<TokenDto> SignUp(SignupDto signupModel);
	Task<UserDto> Register(RegisterDto registerModel, string? registererRole);
	
	Task<IEnumerable<UserIdDto>> GetSpecified(string? userName, string? phone, string? email);
	Task<UserDto> GetById(string? id);

	Task<UserDto> Update(string? id, UserUpdateDto userUpdateDto);
	Task UpdatePassword(string? id, PasswordDto passwordDto);
}
