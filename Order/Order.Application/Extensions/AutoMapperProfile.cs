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
			.ForMember(dest => dest.OrderStatus, 
				opt => opt.MapFrom(src => src.OrderStatus.Name
			));

		CreateMap<Payload, GetPayloadDto>();
		CreateMap<User, GetUserDto>();


		CreateMap<Domain.Entities.Order, GetOrderInfoDto>()
			.ForMember(dest => dest.OrderStatus,
				opt => opt.MapFrom(src => src.OrderStatus.Name
			));
		CreateMap<Payload, GetPayloadOrderDto>();
		CreateMap<User, GetUserInfoDto>();

		CreateMap<UpdateOrderPayloadsDto, Domain.Entities.Order>();
		CreateMap<UpdatePayloadDto, CreatePayloadDto>();
		CreateMap<UpdatePayloadDto, Payload>();
		CreateMap<CreatePayloadDto, Payload>();
		CreateMap<UpdateUserDto, User>();

		CreateMap<UpdateOrderDto, Domain.Entities.Order>();		
	}
}
