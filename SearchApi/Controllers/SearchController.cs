using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SearchApi.Model;
using SearchApi.Services;

namespace SearchApi.Controllers
{
    // ??#11 Controller vs ControllerBase
    // ??#12 string vs IActionResult vs ActionResult<string>
    // ??#13 return View();
    // ??#14 [ValidateAntiForgeryToken]
    // ??#15 [ApiController] vs [Controller]
    // ??#16 Microsoft.AspNetCore.Mvc vs Microsoft.AspNetCore.Http
    // ??#17 routing
    // ??#18 - async practices with http in general and core 6

    [ApiController]
    [Route("[controller]")]
    //[Route("[controller]/[action]")] #??17 - action enforce 'get' action name in url also, no attribute - runtime error (it builds fine)
    public class SearchController : ControllerBase //#11 Controller
    {
        private readonly IGoogleSearchService _googleSearchService;

        public SearchController(IGoogleSearchService googleSearchService)
        {
            _googleSearchService = googleSearchService;
        }

        //[HttpGet(Name = "")] // ??#17 - routing 'Name' not needed, no attribute crashes swagger, url itself still works http://localhost:5283/Search
        [HttpGet]
        public ActionResult Get()
        {
            return Ok("ok text");
        }

        [HttpGet("[action]/{query}")]// ??#17 routing special words
        public async Task<IActionResult> Test(string query)// ??#12
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
