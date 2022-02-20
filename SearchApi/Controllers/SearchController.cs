using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SearchApi.Model;
using SearchApi.Services;

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
    //[Route("[controller]/[action]")] GIT #4 - enforce get in url for the first endpoint also
    public class SearchController : ControllerBase //Controller
    {
        private readonly IGoogleSearchService _googleSearchService;

        public SearchController(IGoogleSearchService googleSearchService)
        {
            _googleSearchService = googleSearchService;
        }

        //[HttpGet(Name = "")] // GIT #3 - routing 'Name' not needed
        [HttpGet]
        public ActionResult Get()
        {
            return Ok("ok text");
        }

        [HttpGet("[action]/{query}")]//GIT #5 routing special words
        public async Task<IActionResult> Test(string query)//GIT #7 diff IActionResult vs ActionResult GIT #8 diff IActionResult vs IActionResult<CustomModel>
        {
            var result = new List<SearchResult>();

            //var result = await googleSearchService.Search(query);
            await foreach (var item in _googleSearchService.Search(query))
            {
                result.Add(item);
            }

            //GIT #2 - one liner instead of string builder spagetti code
            var json = JsonConvert.SerializeObject(result);

            return Ok($"Search result for text: {query} {json}");
        }

        // performance and async related https://docs.microsoft.com/pl-pl/aspnet/core/performance/performance-best-practices?view=aspnetcore-6.0#minimize-exceptions
        // what to focus on from the menu inside:
        // - blocking with Task.Result ...
        // - ltListAsync, enumerable vs async enumerable and System.Text.Json vs Newtonsoft.Json in async
        // - long going tasks in http request - whet to awoid http
        // - minimize exceptions
    }
}
