using AutoMapper;
using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Order.Application.Constants;
using Order.Application.DTOs.UserDTOs;
using Order.Application.Exceptions;
using Order.Application.Extensions;
using Order.Application.Interfaces;
using Order.Domain.Entities;
using Order.Domain.Interfaces;

namespace Order.Application.Services;

public class ConsumerService: BackgroundService 
{
	private readonly IConsumer<long, UserMessageDto> _consumer;	
	private readonly IServiceProvider _serviceProvider;
	
	public ConsumerService(IConsumer<long, UserMessageDto> consumer, IServiceProvider provider)
	{
		_consumer = consumer;
		_serviceProvider = provider;
	}

	protected override Task ExecuteAsync(CancellationToken stoppingToken)
	{
		var topics = new string[]
		{
			Topics.UserCreated,
			Topics.UserDeleted,
			Topics.UserUpdated
		};
		
		_consumer.Subscribe(topics);
		
		return Task.Run(async () =>
		{
			while (!stoppingToken.IsCancellationRequested)
			{
				try
				{
					var consumeResult = _consumer.Consume(stoppingToken);
					
					if (consumeResult != null)
					{			
						using (var scope = _serviceProvider.CreateScope())
						{
							var userService = scope.ServiceProvider.GetRequiredService<IUserService>();
							var mapper = scope.ServiceProvider.GetRequiredService<IMapper>();

							var id = consumeResult.Message.Key;
							var userDto = mapper.Map<UpdateUserDto>(consumeResult.Message.Value);
							
							switch (consumeResult.Topic)
							{
								case Topics.UserCreated:									
									await userService.CreateAsync(id, userDto);
									break;
								case Topics.UserDeleted:
									await userService.DeleteAsync(id);
									break;
								case Topics.UserUpdated:
									await userService.UpdateAsync(id, userDto);
									break;
							}
							
						}						
					}
				}
				catch (OperationCanceledException)
				{
					return;
				}
				catch (ConsumeException)
				{
					// log elk future
				}
				catch (ApiException)
				{
					// log elk future
				}
			}
		}, stoppingToken);	
	}

}
