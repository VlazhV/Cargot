using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Identity.Business.Extensions;

public static class SwaggerExtension
{
	public static IServiceCollection AddSwaggerServices
		(this IServiceCollection services)
	{
		services.AddSwaggerGen(o =>
		{
			o.SwaggerDoc("v1", new OpenApiInfo
			{
				Version = "v1",
				Title = "Cargot Identity",
				Description = "Cargot Identity swagger",
				Contact = new OpenApiContact
				{
					Name = "(C) Cargot llc",
				}

			});

			o.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
			{
				Description = "JWT Authorization header using the Bearer scheme (Example: 'Bearer 12345abcdef')",
				Name = "Authorization",
				In = ParameterLocation.Header,
				Type = SecuritySchemeType.ApiKey,
				Scheme = "Bearer"
			});

			o.AddSecurityRequirement(new OpenApiSecurityRequirement
			{
				{
					new OpenApiSecurityScheme
					{
						Reference = new OpenApiReference
						{
							Type = ReferenceType.SecurityScheme,
							Id = "Bearer",
						}
					},
					Array.Empty<string>()
				}
			});
						
		});
		return services;
	}
}