using SearchApi.Model;
using Google.Apis.Customsearch.v1;
using Google.Apis.Services;

namespace SearchApi.Services
{
    public class GoogleSearchService
    {
        //refactor to at least config (wut better?)
        //GIT #6 securing api keys ...
        private const string apiKey = "AIzaSyDqDB - Fg8ulgOuaFQV2OsobvJ4XHehgc7Q";
        private const string searchEngineId = "001075345664802480471:zfuroutomnu";
        
        private const long PageSize = 10;
        private readonly CustomsearchService _service;

        public GoogleSearchService()
        {
            _service = new CustomsearchService(
                new BaseClientService.Initializer { 
                    ApiKey = apiKey 
                });
        }

        //GIT #1 - full async but how with rest http?
        //public List<SearchResult> Search(string query)
        //public async Task<List<SearchResult>> Search(string query)
        public async IAsyncEnumerable<SearchResult> Search(string query)
        {
            var request = _service.Cse.List(query);

            request.Cx = searchEngineId;
            request.Start = 1;
            request.Num = PageSize;

            //var list = new List<SearchResult>();
            //var search = request.Execute();
            var search = await request.ExecuteAsync();

            foreach (var result in search.Items)
            {
                //list.Add(new SearchResult(result.Title, result.Link));
                yield return new SearchResult(result.Title, result.Link);
            }

            //return list;
        }
    }
}
