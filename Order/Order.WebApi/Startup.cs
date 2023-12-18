using System.Reflection;
using System.Text.RegularExpressions;
using FluentValidation;
using FluentValidation.AspNetCore;
using Order.Application.DTOs.OrderDTOs;
using Order.Application.DTOs.PayloadDTOs;
using Order.Application.DTOs.UserDTOs;
using Order.Application.Interfaces;
using Order.Application.Services;
using Order.Application.Validators;
using Order.Domain.Interfaces;
using Order.Infrastructure.Repositories;

namespace Order.WebApi;

public static class Startup
{
	public static void ConfigureServices(this IServiceCollection services)
	{
		services.AddScoped<IOrderService, OrderService>();
		services.AddScoped<IPayloadService, PayloadService>();
		services.AddScoped<IUserService, UserService>();
	}
	
	public static void ConfigureRepositories(this IServiceCollection services)
	{
		services.AddScoped<IOrderRepository, OrderRepository>();
		services.AddScoped<IPayloadRepository, PayloadRepository>();
		services.AddScoped<IUserRepository, UserRepository>();
	}
	
	public static void ConfigureValidation(this IServiceCollection services)
	{
		services.AddFluentValidationAutoValidation();
		services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
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