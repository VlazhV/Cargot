using AutoMapper;
using Order.Application.DTOs.UserDTOs;
using Order.Application.Exceptions;
using Order.Application.Interfaces;
using Order.Domain.Entities;
using Order.Domain.Interfaces;

namespace Order.Application.Services;

public class UserService: IUserService
{
	private readonly IUserRepository _userRepository;
	private readonly IMapper _mapper;
	
	public UserService(IUserRepository userRepository, IMapper mapper)
	{
		_userRepository = userRepository;
		_mapper = mapper;
	}

	public async Task<GetUserDto> CreateAsync(UpdateUserDto userDto)
	{
		var user = _mapper.Map<User>(userDto);

		user = await _userRepository.CreateAsync(user);

		return _mapper.Map<GetUserDto>(user);
	}

	public async Task DeleteAsync(long id)
	{
		var user = await _userRepository.GetByIdAsync(id)
			?? throw new ApiException("User is not found", ApiException.NotFound);

		await _userRepository.DeleteAsync(user);		
	}

	public async Task<IEnumerable<GetUserDto>> GetAllAsync()
	{
		var users = await _userRepository.GetAllAsync();

		return users.Select(user => _mapper.Map<GetUserDto>(user));		
	}

	public async Task<GetUserInfoDto> GetByIdAsync(long id)
	{
		var user = await _userRepository.GetByIdAsync(id)
			?? throw new ApiException("User is not found", ApiException.NotFound);

		return _mapper.Map<GetUserInfoDto>(user);
	}

	public async Task<GetUserDto> UpdateAsync(long id, UpdateUserDto userDto)
	{
		var user = _mapper.Map<User>(userDto);

		user = await _userRepository.UpdateAsync(user);

		return _mapper.Map<GetUserDto>(user);
	}

}
