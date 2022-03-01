namespace MiddlewareTest
{
    public class TestMiddleware //The middleware class must include
    {
        private readonly RequestDelegate _next;

        public TestMiddleware(RequestDelegate next) //public constructor with a parameter of type RequestDelegate
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)//public method named Invoke or InvokeAsync
        {
            //This method must:Accept a first parameter of type HttpContext.

            await httpContext.Response.WriteAsync(" hello from middleware ");

            await _next(httpContext);//This method must:Return a Task
        }
    }

    public static class TestMiddlewareExtensions
    {
        //Typically, an extension method is created to expose the middleware through IApplicationBuilder
        public static IApplicationBuilder UseTestMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<TestMiddleware>();
        }
    }
}
