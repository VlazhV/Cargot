using Ship.WebApi.Middlewares;

namespace Ship.WebApi.Extensions;

public static class ExceptionHandlerExtension
{
    public static void UseExceptionHandlerMiddleware(this IApplicationBuilder app)
    {
        app.UseMiddleware<ExceptionHandlerMiddleware>();
    }
}