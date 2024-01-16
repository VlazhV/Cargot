using AutoMapper;
using Autopark.gRPC.Requests;
using Ship.Application.DTOs;

namespace Ship.Application.Extensions;

public class AutoMapperProfile: Profile
{
	public AutoMapperProfile()
	{
		CreateMap<Domain.Entities.Ship, GetShipDto>()
			.ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.ToString()));
			
		CreateMap<UpdateShipDto, Domain.Entities.Ship>();

		CreateMap<GenerateShipDto, TimeInterval>()
			.ForMember(dest => dest.Start, opt => opt.MapFrom(src => src.PlannedStart))
			.ForMember(dest => dest.Finish, opt => opt.MapFrom(src => src.PlannedFinish));

		CreateMap<GenerateShipDto, Domain.Entities.Ship>();
	}
}
