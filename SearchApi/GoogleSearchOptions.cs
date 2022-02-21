namespace SearchApi
{
    public class GoogleSearchOptions
    {
        public const string GoogleSearch = "GoogleSearch";

        public string ApiKey { get; set; } = string.Empty;
        public string SearchEngineId { get; set; } = string.Empty;
    }
}
