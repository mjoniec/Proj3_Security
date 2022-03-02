namespace MiddlewareTest
{
    public class OddNumberMiddleware
    {
        private readonly RequestDelegate _next;

        public OddNumberMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            await httpContext.Response.WriteAsync(" hello from odd number middleware ");

            await _next(httpContext);
        }
    }

    public static class OddNumberMiddlewareExtension
    {
        public static IApplicationBuilder UseOddNumberMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<OddNumberMiddleware>();
        }
    }
}
