namespace SearchApi.Model
{
    public class SearchResult
    {
        // ?? enforce non nullable vs anemic type model ...
        public SearchResult(string title, string url)
        {
            Title = title;
            Url = url;
        }

        public string Title { get; private set; }
        public string Url { get; private set; }
    }
}
