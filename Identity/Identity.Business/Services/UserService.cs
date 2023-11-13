using Microsoft.AspNetCore.Identity;
using Identity.Business.Interfaces;
using Identity.Business.DTOs;
using Identity.DataAccess.Entities;
using Identity.DataAccess.Interfaces;
using AutoMapper;
using Identity.DataAccess.Specifications;
using Identity.Business.Exceptions;
using System.Security.Cryptography;
using Identity.Business.Validators;
using FluentValidation;

namespace Identity.Business.Services;

public class UserService : IUserService
{
	private UserManager<IdentityUser<long>> _userManager;
	private IUserRepository _userRepository;
	private ITokenService _tokenService;
	private IMapper _mapper;

	public UserService(UserManager<IdentityUser<long>> userManager,
			IUserRepository userRepository,
			ITokenService tokenService,
			IMapper mapper)
	{
		_userManager = userManager;
		_tokenService = tokenService;
		_userRepository = userRepository;
		_mapper = mapper;
	}

	public async Task<TokenDto> LoginAsync(LoginDto loginModel)
	{
		var validator = new LoginValidator();		
		await validator.ValidateAndThrowAsync(loginModel);	
		
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


	public async Task<UserDto> RegisterAsync(RegisterDto registerDto, string? receptionistRole)
	{
		var validator = new RegisterValidator();
		await validator.ValidateAndThrowAsync(registerDto);
		
		if (receptionistRole != Role.Manager && receptionistRole != Role.Admin)
			throw new ApiException("Unauthorized", ApiException.Unauthorized);
		
		if (receptionistRole == Role.Manager && registerDto.Role == Role.Admin)
			throw new ApiException("No permission", ApiException.BadRequest);
					
		var user = _mapper.Map<IdentityUser<long>>(registerDto);
		
		if (_userRepository.DoesItExist(user))
			throw new ApiException("email, phone number or user name is reserved", ApiException.BadRequest);
		
		var password = GenerateRandom();
		await HandleIdentityResultAsync(_userManager.CreateAsync(user, password));
		
		await HandleIdentityResultAsync(_userManager.AddToRoleAsync(user, registerDto.Role!));
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

	public async Task<TokenDto> SignUpAsync(SignupDto signupDto)
	{
		var validator = new SignupValidator();
		await validator.ValidateAndThrowAsync(signupDto);
			
		var user = _mapper.Map<IdentityUser<long>>(signupDto);

		if (_userRepository.DoesItExist(user))
			throw new ApiException("email, phone number or user name is reserved", ApiException.BadRequest);
		
		await HandleIdentityResultAsync(_userManager.CreateAsync(user, signupDto.Password!));
			
		await _userManager.AddToRoleAsync(user, Role.Client);
		
		var tokenDto = await LoginAsync(_mapper.Map<LoginDto>(signupDto));

		return tokenDto;
	}
	
	public async Task<IEnumerable<UserIdDto>> GetAllWithSpecAsync(SpecDto specDto, string? userRole)
	{		
		if (userRole != Role.Admin && userRole != Role.Manager)
			throw new ApiException("Unauthorized", ApiException.Unauthorized);
			
		var spec = new UserSpecification(specDto.PhoneNumber, specDto.Email, specDto.UserName);
		var users = await _userRepository.GetAllWithSpec(spec);	
		
		var userDtos = users.Select(u => _mapper.Map<UserIdDto>(u)).ToList();
				
		return userDtos;
	}
	
	public async Task<UserDto> GetByIdAsync(string? id, string? userRole)
	{
		if (userRole is not null)
			if (userRole != Role.Admin && userRole != Role.Manager)
				throw new ApiException("Not Found", ApiException.NotFound);
			
		var user = await _userManager.FindByIdAsync(id!) 
			?? throw new ApiException("User is not found", ApiException.NotFound);
			
		var userDto = _mapper.Map<UserDto>(user);
		userDto.Role = (await _userManager.GetRolesAsync(user)).First();
		
		return userDto;
	}

	public async Task<UserDto> UpdateAsync(string? id, UserUpdateDto userUpdateDto)
	{
		var validator = new UserUpdateValidator();
		await validator.ValidateAndThrowAsync(userUpdateDto);
		
		if (id is null)
		 	throw new ApiException("Unauthorized", ApiException.Unauthorized);
		
		var user = await _userManager.FindByIdAsync(id)
			?? throw new ApiException("User is not found", ApiException.NotFound);

		user.Email = userUpdateDto.Email;
		user.PhoneNumber = userUpdateDto.PhoneNumber;
		user.UserName = userUpdateDto.UserName;
		
		await HandleIdentityResultAsync(_userManager.UpdateAsync(user));

		var userDto = _mapper.Map<UserDto>(user);
		userDto.Role = (await _userManager.GetRolesAsync(user)).First();
		
		return userDto;
	}
	
	public async Task UpdatePasswordAsync(string? id, PasswordDto passwordDto)
	{
		var validator = new PasswordValidator();
		await validator.ValidateAndThrowAsync(passwordDto);
		
		if (id is null)
			throw new ApiException("Unauthorized", ApiException.Unauthorized);

		var user = await _userManager.FindByIdAsync(id)
			?? throw new ApiException("User is not found", ApiException.NotFound);

		await HandleIdentityResultAsync(_userManager.ChangePasswordAsync(user, passwordDto.OldPassword!, passwordDto.NewPassword!));
	}
	
	public async Task DeleteAsync(string? id, string? userRole)
	{
		if (userRole != Role.Admin)
			throw new ApiException("Unauthorized", ApiException.Unauthorized);
		
		if (id is null)
			throw new ApiException("BadRequest", ApiException.BadRequest);

		var user = await _userManager.FindByIdAsync(id)
			?? throw new ApiException("User is not found", ApiException.NotFound);

		await HandleIdentityResultAsync(_userManager.DeleteAsync(user));		
	}
	
	private async Task HandleIdentityResultAsync(Task<IdentityResult> taskResult)
	{
		var result = await taskResult;
		if (!result.Succeeded)
		{			
			var message = result.Errors.Select(e => e.Description)
				.Aggregate((a, c) => $"{a}{c}");

			throw new ApiException(message, ApiException.BadRequest);
		}
	}
	
}
