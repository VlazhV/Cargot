using Autopark.Business.Extensions;
using Autopark.DataAccess.Data;
using Identity.Business.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Autopark.Presentation;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<DatabaseContext>(options => 
{
	options.UseSqlServer(
		builder.Configuration.GetConnectionString("Default"),
		sqlBuilder =>
			sqlBuilder.MigrationsAssembly(
				Assembly.GetAssembly(typeof(DatabaseContext))?.GetName().Name)
			);
});

builder.Services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);

builder.Services.ConfigureServices();
builder.Services.ConfigureRepositories();

builder.Services.AddControllers();

builder.Services.ConfigureValidation();

builder.Services.AddIdentityService(builder.Configuration);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
