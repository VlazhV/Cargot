using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Ship.Application.Extensions;
using Ship.Infrastructure.Data;
using Ship.WebApi;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DatabaseContext>(options =>
	options.UseSqlServer(
		builder.Configuration.GetConnectionString("Default"),
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

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
