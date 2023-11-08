using Microsoft.AspNetCore.Identity;
using Identity.Business.Interfaces;
using Identity.Business.DTOs;
using Identity.DataAccess.Entities;
using Identity.DataAccess.Interfaces;
using Microsoft.Extensions.Configuration;
using AutoMapper;
using Identity.DataAccess.Specifications;
using System.Text;
using Identity.Business.Exceptions;
using System.Security.Cryptography;

namespace Identity.Business.Services;

public class UserService : IUserService
{
	private UserManager<IdentityUser<long>> _userManager;
	private IUserRepository _userRepository;
	private ITokenService _tokenService;
	private IConfiguration _configuration;
	private IMapper _mapper;

	public UserService(UserManager<IdentityUser<long>> userManager,
			IUserRepository userRepository,
			ITokenService tokenService,
			IConfiguration configuration,
			IMapper mapper)
	{
		_userManager = userManager;
		_tokenService = tokenService;
		_userRepository = userRepository;
		_configuration = configuration;
		_mapper = mapper;
	}

	public async Task<TokenDto> Login(LoginDto loginModel)
	{
		var user = await _userManager.FindByNameAsync(loginModel.UserName!) 
			?? throw new ApiException("User name and/or password are incorrect", ApiException.NotFound);
		var isPasswordValid = await _userManager.CheckPasswordAsync(user, loginModel.Password!);
		if (!isPasswordValid)
		{
			throw new ApiException("User name and/or password are incorrect", ApiException.NotFound);
		}

		var accessToken = await _tokenService.GetTokenAsync(user);		

		return new TokenDto
		{
			AccessToken = accessToken,			
		};
	}


	public async Task<UserDto> Register(RegisterDto registerDto, string? registererRole)
	{
		if (registererRole is null)
			throw new ApiException("Unauthorized", ApiException.Unauthorized);
		
		if (registererRole == Role.Manager && registerDto.Role == Role.Admin)
			throw new ApiException("No permission", ApiException.BadRequest);
			
		
		var user = _mapper.Map<IdentityUser<long>>(registerDto);
		var password = GenerateRandom();
		var res = await _userManager.CreateAsync(user, password);
		if (!res.Succeeded)
		{
			StringBuilder message = new();
			foreach (var e in res.Errors)
			{
				message.Append(e.Description);
			}
			throw new ApiException(message.ToString(), ApiException.BadRequest);
		}
		
		res = await _userManager.AddToRoleAsync(user, registerDto.Role!);
		if (!res.Succeeded)
		{
			StringBuilder message = new();
			foreach (var e in res.Errors)
			{
				message.Append(e.Description);
			}
			throw new ApiException(message.ToString(), ApiException.BadRequest);
		}
		//send to mail password;
		
		var userDto = _mapper.Map<UserDto>(user);
		userDto.Role = (await _userManager.GetRolesAsync(user)).First();
		return userDto;
	}
	
	private string GenerateRandom()
	{
		byte[] bytes = new byte[20];
		RandomNumberGenerator.Create().GetBytes(bytes);
		return Convert.ToBase64String(bytes);
	}

	public async Task<TokenDto> SignUp(SignupDto signupModel)
	{		
		var user = _mapper.Map<IdentityUser<long>>(signupModel);

		var result = await _userManager.CreateAsync(user, signupModel.Password!);

		if (!result.Succeeded)
		{
			var message = new StringBuilder();
			foreach (var e in result.Errors)
				message.Append(e.Description);
			
			throw new ApiException(message.ToString(), ApiException.BadRequest);	
		}
			

		await _userManager.AddToRoleAsync(user, Role.Client);

		return await Login(new LoginDto
		{
			UserName = signupModel.UserName,
			Password = signupModel.Password
		});
	}
	
	public async Task<IEnumerable<UserIdDto>> GetSpecified(string? userName, string? phone, string? email)
	{						
		var spec = new UserSpecification(phone, email, userName);
		var users = await _userRepository.GetSpecified(spec);	
		
		var userDtos = new List<UserIdDto>();
		foreach (var u in users)
		{
			var userDto = _mapper.Map<UserIdDto>(u);
			userDtos.Add(userDto);
		}
		return userDtos;
	}
	
	public async Task<UserDto> GetById(string? id)
	{
		var user = await _userManager.FindByIdAsync(id!) 
			?? throw new ApiException("User is not found", ApiException.NotFound);
		var userDto = _mapper.Map<UserDto>(user);
		userDto.Role = (await _userManager.GetRolesAsync(user)).First();
		return userDto;
	}

	public async Task<UserDto> Update(string? id, UserUpdateDto userUpdateDto)
	{
		if (id is null)
		 	throw new ApiException("Unauthorized", ApiException.Unauthorized);
		
		var user = await _userManager.FindByIdAsync(id) 
			?? throw new ApiException("User is not found", ApiException.NotFound);

		user.Email = userUpdateDto.Email;		
		user.UserName = userUpdateDto.UserName;
		user.PhoneNumber = userUpdateDto.PhoneNumber;

		var res = await _userManager.UpdateAsync(user);
		if (!res.Succeeded)
		{
			StringBuilder message = new StringBuilder();
			foreach (var e in res.Errors)
			{
				message.Append(e.Description);
			}
			throw new ApiException(message.ToString(), ApiException.BadRequest);
		}

		var userDto = _mapper.Map<UserDto>(user);
		userDto.Role = (await _userManager.GetRolesAsync(user)).First();
		return userDto;
	}
	
	public async Task UpdatePassword(string? id, PasswordDto passwordDto)
	{
		if (id is null)
			throw new ApiException("Unauthorized", ApiException.Unauthorized);

		var user = await _userManager.FindByIdAsync(id)
			?? throw new ApiException("User is not found", ApiException.NotFound);

		var res = await _userManager.ChangePasswordAsync(user, passwordDto.OldPassword!, passwordDto.NewPassword!);
		if (!res.Succeeded)
		{
			StringBuilder message = new();
			foreach (var e in res.Errors)
				message.Append(e.Description);
			throw new ApiException(message.ToString(), ApiException.BadRequest);
		}				
	}
}
