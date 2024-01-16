using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Ship.Application.Extensions;
using Ship.Infrastructure.Data;
using Ship.WebApi;
using MongoDB.Driver;
using MongoDB.EntityFrameworkCore;
using Ship.WebApi.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DatabaseContext>(options =>
	options.UseMongoDB(
		builder.Configuration["MongoDb:ConnectionString"]!,
		builder.Configuration["MongoDb:DatabaseName"]!
	)
);
	
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
builder.Services.RegisterGrpcClient(builder.Configuration);

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
