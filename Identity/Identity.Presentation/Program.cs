using Identity.Business.Interfaces;
using Identity.Business.Services;
using Identity.Business.Extensions;
using Identity.DataAccess.Interfaces;
using Identity.DataAccess.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Identity.DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using FluentValidation.AspNetCore;
using FluentValidation;
using Identity.Business.DTOs;
using Identity.Business.Validators;
using Confluent.Kafka;
using Microsoft.Extensions.Options;
using Confluent.SchemaRegistry;
using Confluent.SchemaRegistry.Serdes;
using Identity.Business.Serializer;



var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DatabaseContext>(options => 
{
	options.UseSqlServer(
		builder.Configuration.GetConnectionString("Default"),
		sqlBuilder => sqlBuilder.MigrationsAssembly(nameof(Identity.DataAccess)));
});
builder.Services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);

builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddControllers();

builder.Services.Configure<ProducerConfig>(
	builder.Configuration.GetSection("Kafka")
);

builder.Services.AddSingleton<IProducer<long, UserIdDto>>(sp =>
{		
	var config = sp.GetRequiredService<IOptions<ProducerConfig>>();
		
	return new ProducerBuilder<long, UserIdDto>(config.Value)
		.SetValueSerializer(new UserIdDtoSerializer())
		.Build();
});

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddScoped<IValidator<LoginDto>, LoginValidator>();
builder.Services.AddScoped<IValidator<PasswordDto>, PasswordValidator>();
builder.Services.AddScoped<IValidator<RegisterDto>, RegisterValidator>();
builder.Services.AddScoped<IValidator<SignupDto>, SignupValidator>();
builder.Services.AddScoped<IValidator<UserUpdateDto>, UserUpdateValidator>();

builder.Services.AddIdentityService(builder.Configuration);

builder.Services.AddAuthorization(options => options.DefaultPolicy =
	new AuthorizationPolicyBuilder (JwtBearerDefaults.AuthenticationScheme)
		.RequireAuthenticatedUser()
		.Build());

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddIdentity<IdentityUser<long>, IdentityRole<long>>()
	.AddEntityFrameworkStores<DatabaseContext>()
	.AddUserManager<UserManager<IdentityUser<long>>>()
	.AddSignInManager<SignInManager<IdentityUser<long>>>();

builder.Services.AddSwaggerServices();
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseExceptionHandlerMiddleware();

app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();
app.Run();
