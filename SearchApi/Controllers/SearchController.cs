using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SearchApi.Model;
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
    //[Route("[controller]/[action]")] GIT #4 - enforce get in url for the first endpoint also
    public class SearchController : ControllerBase //Controller
    {
        //[HttpGet(Name = "")] // GIT #3 - routing 'Name' not needed
        [HttpGet]
        public ActionResult Get()
        {
            return Ok("ok text");
        }

        [HttpGet("[action]/{query}")]//GIT #5 routing special words
        public async Task<IActionResult> Test(string query)
        {
            var googleSearchService = new GoogleSearchService();
            var result = new List<SearchResult>();

            //var result = await googleSearchService.Search(query);
            await foreach (var item in googleSearchService.Search(query))
            {
                result.Add(item);
            }

            //GIT #2 - one liner instead of string builder spagetti code
            var json = JsonConvert.SerializeObject(result);

            return Ok($"Search result for text: {query} {json}");
        }
    }
}
