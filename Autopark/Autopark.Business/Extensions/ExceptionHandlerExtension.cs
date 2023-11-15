using Identity.Business.Middlewares;
using Microsoft.AspNetCore.Builder;

namespace Identity.Business.Extensions;

public static class ExceptionHandlerExtension
{
    public static void UseExceptionHandlerMiddleware(this IApplicationBuilder app)
    {
        app.UseMiddleware<ExceptionHandlerMiddleware>();
    }
}