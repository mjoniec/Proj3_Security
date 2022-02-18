using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SearchApi.Controllers
{
    // ?? Controller vs ControllerBase
    // ?? ActionResult vs string vs ActionResult<int>
    // ?? jak zaimplementowac <T> na customowej klasie ? opcjonalnosc podawania ?
    // ?? return View();
    // ?? [ValidateAntiForgeryToken]
    // ?? [ApiController]
    // ?? Microsoft.AspNetCore.Mvc vs Microsoft.AspNetCore.Http
    // ?? [HttpGet(Name = "GetWeatherForecast")] vs no attribute - potrzebne bo wywala swaggera, aczkolwiek http://localhost:5283/Search dziala
    // ?? [Route("[controller]")] vs no attribute - runtime (it builds fine) error screenshot api must have an attribute route


    [ApiController]
    [Route("[controller]")]
    public class SearchController : ControllerBase //Controller
    {
        // GET: SearchController
        //public ActionResult Index()
        //{
        //    return View();
        //}

        [HttpGet(Name = "")]
        public ActionResult<int> Get()
        {
            return Ok("ok text");
        }
    }
}
