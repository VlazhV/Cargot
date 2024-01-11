using AutoMapper;
using Ship.Application.DTOs;

namespace Ship.Application.Extensions;

public class AutoMapperProfile: Profile
{
	public AutoMapperProfile()
	{
		CreateMap<Domain.Entities.Ship, GetShipDto>();
        CreateMap<UpdateShipDto, Domain.Entities.Ship>();
    }
}
