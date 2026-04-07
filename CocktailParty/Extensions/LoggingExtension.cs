using Microsoft.AspNetCore.Diagnostics;
using Serilog;

namespace CocktailParty.Extensions
{
    public static class LoggingExtension
    {
        public static WebApplicationBuilder AddCustomLogging(this WebApplicationBuilder builder)
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.File(
                    "logs/api-.log",
                    rollingInterval: RollingInterval.Day,
                    retainedFileCountLimit: 14)
                .CreateLogger();

            builder.Host.UseSerilog();

            return builder;
        }

        public static WebApplication UseGlobalExceptionHandling(this WebApplication app)
        {
            app.UseExceptionHandler(errorApp =>
            {
                errorApp.Run(async context =>
                {
                    var exceptionFeature = context.Features
                        .Get<IExceptionHandlerFeature>();

                    if (exceptionFeature != null)
                    {
                        Log.Error(
                            exceptionFeature.Error,
                            "Unhandled exception occurred");
                    }

                    context.Response.StatusCode = 500;
                    context.Response.ContentType = "application/json";

                    await context.Response.WriteAsJsonAsync(new
                    {
                        message = "Internal server error"
                    });
                });
            });

            return app;

        }

    }
}
