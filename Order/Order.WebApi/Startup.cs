using FluentValidation;
using FluentValidation.AspNetCore;
using Order.Application.DTOs.OrderDTOs;
using Order.Application.DTOs.PayloadDTOs;
using Order.Application.Interfaces;
using Order.Application.Services;
using Order.Application.Validators;
using Order.Infrastructure.Interfaces;
using Order.Infrastructure.Repositories;

namespace Order.WebApi;

public static class Startup
{
	public static void ConfigureServices(this IServiceCollection services)
	{
		services.AddScoped<IOrderService, OrderService>();
		services.AddScoped<IPayloadService, PayloadService>();
	}
	
	public static void ConfigureRepositories(this IServiceCollection services)
	{
		services.AddScoped<IOrderRepository, OrderRepository>();
		services.AddScoped<IPayloadRepository, PayloadRepository>();
	}
	
	public static void ConfigureValidation(this IServiceCollection services)
	{
		services.AddFluentValidationAutoValidation();

		services.AddScoped<IValidator<UpdateOrderDto>, UpdateOrderValidator>();
		services.AddScoped<IValidator<UpdatePayloadDto>, UpdatePayloadValidator>();
	}
}