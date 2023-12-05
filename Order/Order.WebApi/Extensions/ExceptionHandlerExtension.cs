using Microsoft.AspNetCore.Builder;
using Order.Application.Middlewares;


namespace Order.Application.Extensions;

public static class ExceptionHandlerExtension
{
    public static void UseExceptionHandlerMiddleware(this IApplicationBuilder app)
    {
        app.UseMiddleware<ExceptionHandlerMiddleware>();
    }
}