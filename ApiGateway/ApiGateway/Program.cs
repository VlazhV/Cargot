using Ocelot.DependencyInjection;
using ApiGateway.Extensions;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
	.AddJsonFile(builder.Configuration["Ocelot:JsonFile"]!, optional: false, reloadOnChange: true)
	.AddEnvironmentVariables();

builder.Services.AddOcelot(builder.Configuration);
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseRouting();


app.UseOcelot().Wait();
app.Run();
