namespace LoggingMiddleware.Controllers
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;

        // Inject a keyed singleton service using [FromKeyedServices]
        public LoggingMiddleware(RequestDelegate next,
            [FromKeyedServices("ServiceName")] MySingletonClass service)
        {
            _next = next;
            // You can use the `service` as needed here
        }

        // Middleware invoke method with a keyed scoped service
        public async Task Invoke(HttpContext context,
    [FromKeyedServices("ServiceName")] MyScopedClass scopedService)
        {
            // Let the rest of the pipeline execute first
            await _next(context);

            // Write to the response after the pipeline finishes
            if (!context.Response.HasStarted) // Check if the response has already started
            {
                await context.Response.WriteAsync($"Using service: {scopedService.GetInfo()}");
            }
        }

    }

    public interface MySingletonClass
    {
        string GetInfo();
    }

    public class MySingletonImplementation : MySingletonClass
    {
        public string GetInfo() => "Singleton Service Info";
    }

    public interface MyScopedClass
    {
        string GetInfo();
    }

    public class MyScopedImplementation : MyScopedClass
    {
        public string GetInfo() => "Scoped Service Info";
    }


}
