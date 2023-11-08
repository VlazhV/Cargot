using Identity.Business.Interfaces;
using Identity.Business.Services;
using Identity.Business.Extensions;
using Identity.DataAccess.Interfaces;
using Identity.DataAccess.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Identity.DataAccess.Data;



var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DatabaseContext>();
builder.Services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);

builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddControllers();

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
