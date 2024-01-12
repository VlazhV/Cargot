using AutoMapper;
using Ship.Application.DTOs;

namespace Ship.Application.Extensions;

public class AutoMapperProfile: Profile
{
	public AutoMapperProfile()
	{
        CreateMap<Domain.Entities.Ship, GetShipDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.ToString()));
        CreateMap<UpdateShipDto, Domain.Entities.Ship>();
	}
}
