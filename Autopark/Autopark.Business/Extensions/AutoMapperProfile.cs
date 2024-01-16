using AutoMapper;
using Autopark.Business.DTOs;
using Autopark.Business.DTOs.AutoparkDTOs;
using Autopark.Business.DTOs.CarDTOs;
using Autopark.Business.DTOs.ScheduleDtos;
using Autopark.Business.DTOs.TrailerDTOs;
using Autopark.DataAccess.Entities;
using Autopark.gRPC.Requests;
using Autopark.gRPC.Responses;

namespace Autopark.Business.Extensions;

public class AutoMapperProfile: Profile
{
	public AutoMapperProfile()
	{
		CreateMap<DataAccess.Entities.Autopark, GetAutoparkDto>();
		CreateMap<Car, GetCarDto>();
		CreateMap<Trailer, GetTrailerDto>();
		CreateMap<CarInShipSchedule, GetScheduleDto>();
		CreateMap<TrailerInShipSchedule, GetScheduleDto>();

		CreateMap<Car, GetCarAutoparkDto>();
		CreateMap<Trailer, GetTrailerAutoparkDto>();
		CreateMap<DataAccess.Entities.Autopark, GetAutoparkVehicleDto>();

		CreateMap<UpdateCarDto, Car>();
		CreateMap<UpdateTrailerDto, Trailer>();
		CreateMap<UpdateAutoparkDto, DataAccess.Entities.Autopark>();
		CreateMap<UpdatePlanScheduleDto, CarInShipSchedule>();
		CreateMap<UpdatePlanScheduleDto, TrailerInShipSchedule>();

		CreateMap<TimeInterval, SpecDto>();
		CreateMap<GetCarAutoparkDto, VehicleResponse>();
		CreateMap<GetTrailerAutoparkDto, VehicleResponse>();
	}
}
