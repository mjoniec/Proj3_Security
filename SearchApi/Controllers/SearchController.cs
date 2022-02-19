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
    //[Route("[controller]/[action]")] enforce get in url for the first endpoint also
    public class SearchController : ControllerBase //Controller
    {
        // GET: SearchController
        //public ActionResult Index()
        //{
        //    return View();
        //}

        //any default attribute[method] to not duplicate Search
        //in method name and route name ? routing reserved names - action, controller ...

        //nazwy tras i parametr Name - niepotrzebne na ten moment

        //[HttpGet(Name = "")] // not needed
        [HttpGet]
        public ActionResult Get()
        {
            return Ok("ok text");
        }

        //[HttpGet("{query}", Name = "[action]")]//-nie dziala, pomija akcje i ustawia na //http://localhost:5283/Search/abc        
        //[HttpGet("{query}", Name = "Test")]//nie dziala nie widzi name nie ma test w url
        //[HttpGet("{query}")]//efekt jak wyzej, nie ma Test w url, ale nie ma niepotrzebnego name
        [HttpGet("[action]/{query}")]//ok
        public ActionResult Test(string query)
        {
            return Ok($"Search for text: {query}");
        }
    }
}
