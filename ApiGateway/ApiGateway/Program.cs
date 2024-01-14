using Ocelot.DependencyInjection;
using ApiGateway.Extensions;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
	.AddJsonFile(builder.Configuration["Ocelot:JsonFile"]!, optional: false, reloadOnChange: true)
	.AddEnvironmentVariables();

builder.Services.AddOcelot(builder.Configuration);
builder.Services.AddSwaggerForOcelot(builder.Configuration);
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.ConfigureCors(builder.Configuration);

var app = builder.Build();

app.UseCors();
app.UseRouting();

app.UseSwaggerForOcelotUI(opt =>
{
	app.UseOcelotSwagger(builder.Configuration);
});

app.UseOcelot().Wait();
app.Run();
