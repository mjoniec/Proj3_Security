using Microsoft.AspNetCore.Mvc;

namespace MiddlewareTest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        [HttpGet]
        public void Get()
        {
            HttpContext.Response.WriteAsync(" hi from controller ");
        }
    }
}