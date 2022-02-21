using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SearchApi.Model;
using SearchApi.Services;

namespace SearchApi.Controllers
{
    // ??#11 Controller vs ControllerBase
    // ??#12 ActionResult vs string vs ActionResult<string>
    // ??#13 return View();
    // ??#14 [ValidateAntiForgeryToken]
    // ??#15 [ApiController]
    // ??#16 Microsoft.AspNetCore.Mvc vs Microsoft.AspNetCore.Http
    
    // ??#17 routing
    // [HttpGet(Name = "GetWeatherForecast")] vs no attribute - potrzebne bo wywala swaggera, aczkolwiek http://localhost:5283/Search dziala
    // [Route("[controller]")] vs no attribute - runtime (it builds fine) error screenshot api must have an attribute route

    // ??#18 - async practices with http in general and core 6

    [ApiController]
    [Route("[controller]")]
    //[Route("[controller]/[action]")] GIT #17 - enforce get in url for the first endpoint also
    public class SearchController : ControllerBase //#11 Controller
    {
        private readonly IGoogleSearchService _googleSearchService;

        public SearchController(IGoogleSearchService googleSearchService)
        {
            _googleSearchService = googleSearchService;
        }

        //[HttpGet(Name = "")] // ??#17 - routing 'Name' not needed
        [HttpGet]
        public ActionResult Get()
        {
            return Ok("ok text");
        }

        [HttpGet("[action]/{query}")]// ??#17 routing special words
        public async Task<IActionResult> Test(string query)// ??#12 diff IActionResult vs ActionResult GIT #8 diff IActionResult vs IActionResult<CustomModel>
        {
            var result = new List<SearchResult>();

            // ??#18 
            //var result = await googleSearchService.Search(query);
            await foreach (var item in _googleSearchService.Search(query))
            {
                result.Add(item);
            }

            //one liner instead of string builder spagetti code
            var json = JsonConvert.SerializeObject(result);

            return Ok($"Search result for text: {query} {json}");
        }
    }
}
