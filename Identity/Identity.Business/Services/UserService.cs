using Microsoft.AspNetCore.Identity;
using Identity.Business.Interfaces;
using Identity.Business.DTOs;
using Identity.DataAccess.Entities;
using Identity.DataAccess.Interfaces;
using AutoMapper;
using Identity.DataAccess.Specifications;
using Identity.Business.Exceptions;
using System.Security.Cryptography;
using Confluent.Kafka;
using Identity.Business.Constants;

namespace Identity.Business.Services;

public class UserService : IUserService
{
	private readonly IUserRepository _userRepository;
	private readonly ITokenService _tokenService;
	private readonly IMapper _mapper;
	private readonly IProducer<long, UserIdDto> _producer;

	public UserService(
			IProducer<long, UserIdDto> producer,
			IUserRepository userRepository,
			ITokenService tokenService,
			IMapper mapper)
	{
		_producer = producer;
		_tokenService = tokenService;
		_userRepository = userRepository;
		_mapper = mapper;
	}

	public async Task<TokenDto> LoginAsync(LoginDto loginModel)
	{
		var user = await _userRepository.FindByNameAsync(loginModel.UserName!) 
			?? throw new ApiException("User name and/or password are incorrect", ApiException.NotFound);
	
		var isPasswordValid = await _userRepository.CheckPasswordAsync(user, loginModel.Password!);
	
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
		if (receptionistRole != Role.Manager && receptionistRole != Role.Admin)
			throw new ApiException("Unauthorized", ApiException.Unauthorized);
		
		if (receptionistRole == Role.Manager && registerDto.Role == Role.Admin)
			throw new ApiException("No permission", ApiException.BadRequest);
					
		var user = _mapper.Map<IdentityUser<long>>(registerDto);
		
		if (_userRepository.DoesItExist(user))
			throw new ApiException("email, phone number or user name is reserved", ApiException.BadRequest);
		
		var password = GenerateRandom();
		await HandleIdentityResultAsync(_userRepository.CreateAsync(user, password));
		
		await HandleIdentityResultAsync(_userRepository.AddToRoleAsync(user, registerDto.Role!));
		//send to mail password;
		
		var userDto = _mapper.Map<UserDto>(user);
		userDto.Role = (await _userRepository.GetRolesAsync(user)).First();
	
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
		var user = _mapper.Map<IdentityUser<long>>(signupDto);

		if (_userRepository.DoesItExist(user))
			throw new ApiException("email, phone number or user name is reserved", ApiException.BadRequest);
		
		await HandleIdentityResultAsync(_userRepository.CreateAsync(user, signupDto.Password!));			
		await _userRepository.AddToRoleAsync(user, Role.Client);

		await ProduceAsync(user, Topics.UserCreated);
		
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
			
		var user = await _userRepository.FindByIdAsync(id!) 
			?? throw new ApiException("User is not found", ApiException.NotFound);
			
		var userDto = _mapper.Map<UserDto>(user);
		userDto.Role = (await _userRepository.GetRolesAsync(user)).First();
		
		return userDto;
	}

	public async Task<UserDto> UpdateAsync(string? id, UserUpdateDto userUpdateDto)
	{
		if (id is null)
		 	throw new ApiException("Unauthorized", ApiException.Unauthorized);
		
		var user = await _userRepository.FindByIdAsync(id)
			?? throw new ApiException("User is not found", ApiException.NotFound);

		user.Email = userUpdateDto.Email;
		user.PhoneNumber = userUpdateDto.PhoneNumber;
		user.UserName = userUpdateDto.UserName;
		
		await HandleIdentityResultAsync(_userRepository.UpdateAsync(user));

		var userDto = _mapper.Map<UserDto>(user);
		userDto.Role = (await _userRepository.GetRolesAsync(user)).First();

		await ProduceAsync(user, Topics.UserUpdated);
		
		return userDto;
	}
	
	public async Task UpdatePasswordAsync(string? id, PasswordDto passwordDto)
	{
		if (id is null)
			throw new ApiException("Unauthorized", ApiException.Unauthorized);

		var user = await _userRepository.FindByIdAsync(id)
			?? throw new ApiException("User is not found", ApiException.NotFound);

		await HandleIdentityResultAsync(_userRepository.ChangePasswordAsync(user, passwordDto.OldPassword!, passwordDto.NewPassword!));
	}
	
	public async Task DeleteAsync(string? id, string? userRole)
	{
		if (userRole != Role.Admin)
			throw new ApiException("Unauthorized", ApiException.Unauthorized);
		
		if (id is null)
			throw new ApiException("BadRequest", ApiException.BadRequest);

		var user = await _userRepository.FindByIdAsync(id)
			?? throw new ApiException("User is not found", ApiException.NotFound);

		await HandleIdentityResultAsync(_userRepository.DeleteAsync(user));

		await ProduceAsync(user, Topics.UserDeleted);
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
	
	private async Task ProduceAsync(IdentityUser<long> user, string topic)
	{
		var userIdDto = _mapper.Map<UserIdDto>(user);
		var message = new Message<long, UserIdDto>
		{
			Key = userIdDto.Id,
			Value = userIdDto
		};

		await _producer.ProduceAsync(topic, message);
	}
}
