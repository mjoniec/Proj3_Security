using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SearchApi.Services;
using System.Text;

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
            var googleSearchService = new GoogleSearchService();

            var list = googleSearchService.Search(query);

            //var s = String.Join(", ", list.SelectMany(o => o.Title))

            var sb = new StringBuilder();
            
            foreach (var item in list)
            {
                sb.Append(item.Title);
                sb.Append(" ");
                sb.Append(item.Url);
                sb.AppendLine();
            }

            return Ok($"Search for text: {query} {sb}");
        }
    }
}
