namespace MiddlewareTest
{
    public class EvenNumberMiddleware
    {
        private readonly RequestDelegate _next;

        public EvenNumberMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            await httpContext.Response.WriteAsync(" hello from even number middleware ");

            await _next(httpContext);
        }
    }

    public static class EvenNumberMiddlewareExtension
    {
        public static IApplicationBuilder UseEvenNumberMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<EvenNumberMiddleware>();
        }
    }
}
