using System.Reflection;
using FluentValidation;
using FluentValidation.AspNetCore;
using Ship.Application.Exceptions;
using Ship.Application.Interfaces;
using Ship.Application.Services;
using Ship.Application.Validators;
using Ship.Domain.Interfaces;
using Ship.Infrastructure.Repositories;

namespace Ship.WebApi;

public static class Startup
{
	public static void ConfigureServices(this IServiceCollection services)
	{
        services.AddScoped<IShipService, ShipService>();
    }
	
	public static void ConfigureRepositories(this IServiceCollection services)
	{
		services.AddScoped<IShipRepository, ShipRepository>();
	}
	
	public static void ConfigureValidation(this IServiceCollection services)
	{
		services.AddFluentValidationAutoValidation();
		services.AddValidatorsFromAssembly(Assembly.GetAssembly(typeof(UpdateShipValidator)));
	}
}
