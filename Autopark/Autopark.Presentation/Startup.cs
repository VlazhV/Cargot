using Autopark.Business.DTOs;
using Autopark.Business.DTOs.AutoparkDTOs;
using Autopark.Business.DTOs.CarDTOs;
using Autopark.Business.DTOs.ScheduleDtos;
using Autopark.Business.DTOs.TrailerDTOs;
using Autopark.Business.Interfaces;
using Autopark.Business.Services;
using Autopark.Business.Validators;
using Autopark.DataAccess.Repositories;
using FluentValidation;
using FluentValidation.AspNetCore;

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
		services.AddScoped<CarRepository>();
		services.AddScoped<TrailerRepository>();
		services.AddScoped<AutoparkRepository>();
		services.AddScoped<CarInShipScheduleRepository>();
		services.AddScoped<TrailerInShipScheduleRepository>();
	}
	
	public static void ConfigureValidation(this IServiceCollection services)
	{
		services.AddFluentValidationAutoValidation();
		services.AddScoped<IValidator<UpdateAutoparkDto>, UpdateAutoparkValidator>();
		services.AddScoped<IValidator<UpdateCarDto>, UpdateCarValidator>();
		services.AddScoped<IValidator<UpdateTrailerDto>, UpdateTrailerValidator>();
		services.AddScoped<IValidator<UpdatePlanScheduleDto>, UpdatePlanScheduleValidator>();
		services.AddScoped<IValidator<SpecDto>, SpecValidator>();
	}
}
