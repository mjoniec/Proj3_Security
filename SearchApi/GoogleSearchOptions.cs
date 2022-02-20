namespace SearchApi
{
    // ?? Options pattern in ASP.NET Core https://docs.microsoft.com/en-us/aspnet/core/fundamentals/configuration/options?view=aspnetcore-6.0

    public class GoogleSearchOptions
    {
        public const string GoogleSearch = "GoogleSearch";

        public string ApiKey { get; set; } = string.Empty;
        public string SearchEngineId { get; set; } = string.Empty;
    }
}
