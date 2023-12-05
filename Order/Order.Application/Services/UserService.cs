using AutoMapper;
using Order.Application.Constants;
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
		
		if (await _userRepository.DoesItExistAsync(user))
		{
			throw new ApiException(Messages.EmailPhoneNameIsReserved, ApiException.BadRequest);	
		}

		user = await _userRepository.CreateAsync(user);
		await _userRepository.SaveChangesAsync();

		return _mapper.Map<GetUserDto>(user);
	}

	public async Task DeleteAsync(long id)
	{
		var user = await _userRepository.GetByIdAsync(id)
			?? throw new ApiException(Messages.UserIsNotFound, ApiException.NotFound);

		_userRepository.Delete(user);
		await _userRepository.SaveChangesAsync();
	}

	public async Task<IEnumerable<GetUserDto>> GetAllAsync()
	{
		var users = await _userRepository.GetAllAsync();
		
		return _mapper.Map<IEnumerable<GetUserDto>>(users);
	}

	public async Task<GetUserInfoDto> GetByIdAsync(long id)
	{
		var user = await _userRepository.GetByIdAsync(id)
			?? throw new ApiException(Messages.UserIsNotFound, ApiException.NotFound);

		return _mapper.Map<GetUserInfoDto>(user);
	}

	public async Task<GetUserDto> UpdateAsync(long id, UpdateUserDto userDto)
	{
		if (! await _userRepository.DoesItExistAsync(id))
		{
			throw new ApiException(Messages.UserIsNotFound, ApiException.NotFound);
		}
		
		var user = _mapper.Map<User>(userDto);
		
		if (await _userRepository.DoesItExistAsync(user))
		{
			throw new ApiException(Messages.EmailPhoneNameIsReserved, ApiException.BadRequest);
		}
		
		user.Id = id;
		user = _userRepository.Update(user);
		
		await _userRepository.SaveChangesAsync();

		return _mapper.Map<GetUserDto>(user);
	}
}
