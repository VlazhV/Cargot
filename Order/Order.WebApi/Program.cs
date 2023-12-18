using System.Reflection;
using DotNetEnv;
using Microsoft.EntityFrameworkCore;
using Order.Application.Extensions;
using Order.Infrastructure.Data;
using Order.WebApi;

var builder = WebApplication.CreateBuilder(args);

Env.Load();

var variant = args.GetConnectionStringVariant();
var connectionString = builder.Configuration.GetConfiguredConnectionString(variant);

builder.Services.AddDbContext<DatabaseContext>(options =>
	options.UseSqlServer(
		connectionString,
		sqlBuilder => sqlBuilder.MigrationsAssembly(
			Assembly.GetAssembly(typeof(DatabaseContext))!.GetName().Name
		)
	));

builder.Services.AddAutoMapper(Assembly.GetAssembly(typeof(AutoMapperProfile)));

builder.Services.ConfigureServices();
builder.Services.ConfigureRepositories();

builder.Services.AddRouting(options => {
	options.LowercaseUrls = true;
	options.LowercaseQueryStrings = true;
});

builder.Services.AddControllers();
builder.Services.ConfigureValidation();

builder.Services.AddIdentityService(builder.Configuration);

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerServices();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseExceptionHandlerMiddleware();
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
