using System.Reflection;
using Autopark.Business.Interfaces;
using Autopark.Business.Services;
using Autopark.Business.Validators;
using Autopark.DataAccess.Interfaces;
using Autopark.DataAccess.Repositories;
using FluentValidation;
using FluentValidation.AspNetCore;
using ProtoBuf.Grpc.Server;

namespace Autopark.Presentation;

public static class Startup
{
	public static void ConfigureServices(this IServiceCollection services)
	{
		services.AddScoped<ICarService, CarService>();
		services.AddScoped<ITrailerService, TrailerService>();
		services.AddScoped<IAutoparkService, AutoparkService>();
		services.AddScoped<ICarScheduleService, CarScheduleService>();
		services.AddScoped<ITrailerScheduleService, TrailerScheduleService>();
	}
	
	public static void ConfigureRepositories(this IServiceCollection services)
	{
		services.AddScoped<ICarRepository, CarRepository>();
		services.AddScoped<ITrailerRepository, TrailerRepository>();
		services.AddScoped<IAutoparkRepository, AutoparkRepository>();
		services.AddScoped<ICarInShipScheduleRepository, CarInShipScheduleRepository>();
		services.AddScoped<ITrailerInShipScheduleRepository, TrailerInShipScheduleRepository>();
	}
	
	public static void ConfigureValidation(this IServiceCollection services)
	{
		services.AddFluentValidationAutoValidation();
		services.AddValidatorsFromAssembly(Assembly.GetAssembly(typeof(UpdateAutoparkValidator)));
	}
	
	public static void RegisterGrpcService(this IServiceCollection services)
	{
		services.AddCodeFirstGrpc();
	}
	
	public static void UserGrpcService(this IApplicationBuilder app)
	{
		app.UseEndpoints(endpoints =>
		{
			endpoints.MapGrpcService<gRPC.Services.AutoparkService>();
		});
	}
}
