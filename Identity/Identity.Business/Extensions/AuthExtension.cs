using System.Security.Authentication;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Identity.Business.Extensions;

public static class AuthExtension
{
	public static IServiceCollection AddIdentityService
		(this IServiceCollection services, IConfiguration configuration)	
	{		
		services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
			.AddJwtBearer(options =>
			{				

				options.TokenValidationParameters = new()
				{
					ValidateIssuerSigningKey = true,
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Secret"]!)),

					ValidateIssuer = true,
					ValidIssuer = configuration["Jwt:Issuer"],
					
					ValidateAudience = true,
					ValidAudience = configuration["Jwt:Audience"],
					
					ValidateLifetime = true
				};
			});


		return services;
	}
}