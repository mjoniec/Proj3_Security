using SearchApi.Model;

namespace SearchApi.Services
{
    public interface IGoogleSearchService
    {
        IAsyncEnumerable<SearchResult> Search(string query);
    }
}
