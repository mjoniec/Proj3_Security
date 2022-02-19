using SearchApi.Model;
using Google.Apis.Customsearch.v1;
using Google.Apis.Services;

namespace SearchApi.Services
{
    public class GoogleSearchService
    {
        //refactor to at least config (wut better?)
        private const string apiKey = "AIzaSyDqDB - Fg8ulgOuaFQV2OsobvJ4XHehgc7Q";
        private const string searchEngineId = "001075345664802480471:zfuroutomnu";
        
        private const long PageSize = 10;//refactor to async
        private readonly CustomsearchService _service;

        public GoogleSearchService()
        {
            _service = new CustomsearchService(
                new BaseClientService.Initializer { 
                    ApiKey = apiKey 
                });
        }

        //redo this method
        public List<SearchResult> Search(string query)
        {
            var listRequest = _service.Cse.List(query);

            listRequest.Cx = searchEngineId;
            listRequest.Start = 1;
            listRequest.Num = PageSize;

            var list = new List<SearchResult>();

            //yeld return maybe?? async somehow...
            Google.Apis.Customsearch.v1.Data.Search search = listRequest.Execute();
            //var search = listRequest.ExecuteAsync(); //TODO async in asp core http rest api

            foreach (Google.Apis.Customsearch.v1.Data.Result result in search.Items)
            {
                list.Add(new SearchResult(result.Title, result.Link));
            }

            return list;
        }
    }
}
