using Autopark.Business.Extensions;
using Autopark.Business.Interfaces;
using Autopark.Business.Services;
using Autopark.DataAccess.Data;
using Autopark.DataAccess.Interfaces;
using Autopark.DataAccess.Repositories;
using Identity.Business.Extensions;
using Microsoft.EntityFrameworkCore;
using FluentValidation.AspNetCore;
using FluentValidation;
using Autopark.Business.DTOs.AutoparkDTOs;
using Autopark.Business.Validators;
using Autopark.Business.DTOs.CarDTOs;
using Autopark.Business.DTOs.TrailerDTOs;
using System.Reflection;

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

builder.Services.AddScoped<ICarService, CarService>();
builder.Services.AddScoped<ITrailerService, TrailerService>();
builder.Services.AddScoped<IAutoparkService, AutoparkService>();

builder.Services.AddScoped<ICarRepository, CarRepository>();
builder.Services.AddScoped<ITrailerRepository, TrailerRepository>();
builder.Services.AddScoped<IAutoparkRepository, AutoparkRepository>();
builder.Services.AddScoped<ICarInShipSheduleRepository, CarInShipSheduleRepository>();
builder.Services.AddScoped<ITrailerInShipSheduleRepository, TrailerInShipSheduleRepository>();

builder.Services.AddControllers();

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddScoped<IValidator<UpdateAutoparkDto>, UpdateAutoparkValidator>();
builder.Services.AddScoped<IValidator<UpdateCarDto>, UpdateCarValidator>();
builder.Services.AddScoped<IValidator<UpdateTrailerDto>, UpdateTrailerValidator>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseExceptionHandlerMiddleware();
app.UseAuthorization();

app.MapControllers();

app.Run();
