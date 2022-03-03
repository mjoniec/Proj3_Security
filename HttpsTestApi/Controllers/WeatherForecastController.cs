using Microsoft.AspNetCore.Mvc;

namespace HttpsTestApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        //http://localhost:64930/weatherforecast
        //https://localhost:44398/weatherforecast
        public IActionResult Get()
        {
            return Ok("info");
        }
    }
}