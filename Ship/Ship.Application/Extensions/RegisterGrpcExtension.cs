using Grpc.Net.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProtoBuf.Grpc.Client;
using Autopark.gRPC.Services;

namespace Ship.Application.Extensions;

public static class RegisterGrpcExtension
{
	public static void RegisterGrpcClient
		(this IServiceCollection services, 
		IConfiguration configuration)
	{
		var endpoint = configuration["GrpcService:EndPoint"]!;
		var channel = GrpcChannel.ForAddress(endpoint);

		services.AddSingleton(channel);
		
		services.AddScoped(provider => 
			channel.CreateGrpcService<IAutoparkService>()
		);
	}
}
