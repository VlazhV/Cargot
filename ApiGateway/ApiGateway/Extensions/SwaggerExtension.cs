namespace ApiGateway.Extensions;

public static class SwaggerExtension
{
    public static void UseOcelotSwagger(this IApplicationBuilder app, IConfiguration config)
    {
        app.UseSwaggerForOcelotUI(opt =>
        {
            opt.PathToSwaggerGenerator = config["Ocelot:PathToSwaggerGen"];
        });
    }
}