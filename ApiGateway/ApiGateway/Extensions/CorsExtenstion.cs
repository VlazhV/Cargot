namespace ApiGateway.Extensions;

public static class CorsExtenstion
{
	public static void ConfigureCors(this IServiceCollection services, IConfiguration configuration)
	{
		var apiGateway = configuration["AllowedOrigins:ApiGateway"]!;
		var identity = configuration["AllowedOrigins:Identity"]!;
		var autopark = configuration["AllowedOrigins:Autopark"]!;
		var order = configuration["AllowedOrigins:Order"]!;
		var ship = configuration["AllowedOrigins:Ship"]!;

		services.AddCors(options =>
		{
			options.AddDefaultPolicy(builder =>
			{
				builder
					.WithOrigins(apiGateway, autopark, order, identity, ship)
					.AllowAnyHeader()
					.AllowAnyMethod()
					.AllowCredentials();
			});
		});
	}
}
