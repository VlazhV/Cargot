using System.Reflection;
using System.Text.RegularExpressions;
using Autopark.Business.Interfaces;
using Autopark.Business.Services;
using Autopark.Business.Validators;
using Autopark.DataAccess.Interfaces;
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
	
	public static string GetConfiguredConnectionString(this IConfiguration configuration, string variant)
	{
		var connectionString = configuration.GetConnectionString(variant);	
		
		if (string.IsNullOrEmpty(connectionString))
		{
			throw new ArgumentException("There is no such variant of connection string");
		}		
				
		var matches = Regex.Matches(connectionString, @"\$\{\w+\}");
		
		foreach (Match match in matches)
		{
			var arg = match.Value;
			var envVariable = Environment.GetEnvironmentVariable(arg[2..^1]);
			
			if (envVariable is null)
			{
				throw new ArgumentException($"There is no environment variable {arg[2..^1]}");
			}

			connectionString = connectionString.Replace(arg, envVariable);
		}	

		return connectionString;
	}
	
	public static string GetConnectionStringVariant(this string[] args)
	{
		if (args.Length >= 1)
			return args[0];

		return "Default";
	}
}
