using System.Reflection;
using FluentValidation;
using FluentValidation.AspNetCore;
using Ship.Application.Exceptions;
using Ship.Domain.Interfaces;
using Ship.Infrastructure.Repositories;

namespace Ship.WebApi;

public static class Startup
{
	public static void ConfigureServices(this IServiceCollection services)
	{
		
	}
	
	public static void ConfigureRepositories(this IServiceCollection services)
	{
		services.AddScoped<IShipRepository, ShipRepository>();
		services.AddScoped<ICarRepository, CarRepository>();
		services.AddScoped<IDriverRepository, DriverRepository>();
		services.AddScoped<IRouteStopRepository, RouteStopRepository>();
		services.AddScoped<ITrailerRepository, TrailerRepository>();
	}
	
	public static void ConfigureValidation(this IServiceCollection services)
	{
		services.AddFluentValidationAutoValidation();
		services.AddValidatorsFromAssembly(Assembly.GetAssembly(typeof(ApiException))); //!
	}
}
