using System.Reflection;
using Confluent.Kafka;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.Options;
using Order.Application.Deserializers;
using Order.Application.DTOs.UserDTOs;
using Order.Application.Interfaces;
using Order.Application.Services;
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
	
	public static void ConfigureBroker(this IServiceCollection services, IConfiguration configuration)
	{		
		services.Configure<ConsumerConfig>(configuration.GetSection("Kafka"));
		
		services.AddTransient<IConsumer<long, UserMessageDto>>(provider =>
		{			
			var config = provider.GetRequiredService<IOptions<ConsumerConfig>>().Value;

			var consumer = new ConsumerBuilder<long, UserMessageDto>(config)
				.SetValueDeserializer(new UserMessageDtoDeserializer())
				.Build();

			return consumer;			
		});

		services.AddHostedService<ConsumerService>();
	}
}