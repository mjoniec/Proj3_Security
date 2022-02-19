using SearchApi.Model;

namespace SearchApi.Services
{
    public class GoogleSearchService
    {
        //refactor to at least config (wut better?)
        private const string apiKey = "AIzaSyDqDB - Fg8ulgOuaFQV2OsobvJ4XHehgc7Q";
        private const string searchEngineId = "001075345664802480471:zfuroutomnu";
        
        private const long PageSize = 10;//refactor to async
        private readonly Google.Apis.Customsearch.v1.CustomsearchService _service;

        public GoogleSearchService()
        {
            _service = new Google.Apis.Customsearch.v1.CustomsearchService(
                new Google.Apis.Services.BaseClientService.Initializer { ApiKey = apiKey });
        }

        //redo this method
        public List<SearchResult> Search(string query, long maxResults = 12)
        {
            //var listRequest = _service.Cse.List(query);

            var listRequest = _service.Cse.List(query);

            listRequest.Cx = searchEngineId;

            long pageNumber = 1;
            bool takeNextPage = true;
            var list = new List<SearchResult>();

            while (takeNextPage)
            {
                listRequest.Start = (pageNumber++ - 1) * PageSize + 1;

                if (listRequest.Start + PageSize > maxResults)
                {
                    listRequest.Num = maxResults - listRequest.Start + 1;
                    takeNextPage = false;
                }
                else
                {
                    listRequest.Num = PageSize;
                }

                Google.Apis.Customsearch.v1.Data.Search search = listRequest.Execute();

                foreach (Google.Apis.Customsearch.v1.Data.Result result in search.Items)
                {
                    list.Add(new SearchResult(result.Title, result.Link));
                }
            }

            return list;
        }
    }
}
