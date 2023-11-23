using AutoMapper;
using Order.Application.DTOs.OrderDTOs;
using Order.Application.DTOs.PayloadDTOs;
using Order.Application.DTOs.UserDTOs;
using Order.Domain.Entities;

namespace Order.Application.Extensions;

public class AutoMapperProfile: Profile
{
	public AutoMapperProfile()
	{
		CreateMap<Domain.Entities.Order, GetOrderDto>()
			.ForMember(dest => dest.OrderStatus, o => o.MapFrom(src => src.OrderStatus.Name));

		CreateMap<Payload, GetPayloadDto>();
		CreateMap<User, GetUserDto>();


		CreateMap<Domain.Entities.Order, GetOrderInfoDto>();
		CreateMap<Payload, GetPayloadOrderDto>();
		CreateMap<User, GetUserInfoDto>();

		CreateMap<UpdateOrderDto, Domain.Entities.Order>();		
	}
}
