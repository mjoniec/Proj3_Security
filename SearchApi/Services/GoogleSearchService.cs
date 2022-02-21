using SearchApi.Model;
using Google.Apis.Customsearch.v1;
using Google.Apis.Services;

namespace SearchApi.Services
{
    public class GoogleSearchService : IGoogleSearchService
    {
        private const long PageSize = 10;
        private readonly CustomsearchService _service;
        private readonly GoogleSearchOptions _googleSearchOptions = new();// ??#22 on this this shotened structure ... Core 6 ?

        public GoogleSearchService(IConfiguration configuration)
        {
            // ??#19 - Options pattern in ASP.NET Core https://docs.microsoft.com/en-us/aspnet/core/fundamentals/configuration/options?view=aspnetcore-6.0
            // ??#20 - storing and securing api keys ... read recommendation on storing and encrypting api keys in configs
            // ??#21 - automapper in core 6
            // ??#22 - new() in core 6
            configuration.GetSection(GoogleSearchOptions.GoogleSearch)
                .Bind(_googleSearchOptions);

            _service = new CustomsearchService(
                new BaseClientService.Initializer { 
                    ApiKey = _googleSearchOptions.ApiKey
                });
        }

        // ??#18 - full async but how with rest http?
        //public List<SearchResult> Search(string query)
        //public async Task<List<SearchResult>> Search(string query)
        public async IAsyncEnumerable<SearchResult> Search(string query)
        {
            var request = _service.Cse.List(query);

            request.Cx = _googleSearchOptions.SearchEngineId;
            request.Start = 1;
            request.Num = PageSize;

            // ??#18
            //var list = new List<SearchResult>();
            //var search = request.Execute();
            var search = await request.ExecuteAsync();

            foreach (var result in search.Items)
            {
                //list.Add(new SearchResult(result.Title, result.Link));
                yield return new SearchResult(result.Title, result.Link);// ??#21 not worth to use automapper for this one line ... 
            }

            //return list;
        }
    }
}
