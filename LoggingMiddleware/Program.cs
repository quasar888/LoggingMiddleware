using LoggingMiddleware.Controllers;

namespace LoggingMiddleware
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Register keyed services BEFORE app.Build()
            builder.Services.AddKeyedSingleton<MySingletonClass, MySingletonImplementation>("ServiceName");
            builder.Services.AddKeyedScoped<MyScopedClass, MyScopedImplementation>("ServiceName");

            var app = builder.Build();

            // Use middleware
            app.UseMiddleware<LoggingMiddleware.Controllers.LoggingMiddleware>();

            // Map a default route
            app.MapGet("/", () => "Hello World!");

            // Start the application
            app.Run();
        }

    }
}
