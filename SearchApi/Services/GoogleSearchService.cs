using SearchApi.Model;
using Google.Apis.Customsearch.v1;
using Google.Apis.Services;

namespace SearchApi.Services
{
    public class GoogleSearchService : IGoogleSearchService
    {
        private const long PageSize = 10;
        private readonly CustomsearchService _service;
        private readonly GoogleSearchOptions _googleSearchOptions = new();// ?? on this this shotened structure ... Core 6 ?

        public GoogleSearchService(IConfiguration configuration)
        {
            //GIT #6 securing api keys ... read recommendation on storing and encrypting api keys in configs
            configuration.GetSection(GoogleSearchOptions.GoogleSearch)
                .Bind(_googleSearchOptions);

            _service = new CustomsearchService(
                new BaseClientService.Initializer { 
                    ApiKey = _googleSearchOptions.ApiKey
                });
        }

        //GIT #1 - full async but how with rest http?
        //public List<SearchResult> Search(string query)
        //public async Task<List<SearchResult>> Search(string query)
        public async IAsyncEnumerable<SearchResult> Search(string query)
        {
            var request = _service.Cse.List(query);

            request.Cx = _googleSearchOptions.SearchEngineId;
            request.Start = 1;
            request.Num = PageSize;

            //var list = new List<SearchResult>();
            //var search = request.Execute();
            var search = await request.ExecuteAsync();

            foreach (var result in search.Items)
            {
                //list.Add(new SearchResult(result.Title, result.Link));
                yield return new SearchResult(result.Title, result.Link);//not worth to use automapper for this one line ... 
            }

            //return list;
        }
    }
}
