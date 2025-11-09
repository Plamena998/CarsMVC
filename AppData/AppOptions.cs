namespace Core.Models
{
    // Клас за мапване на ApiParameters от appsettings.json
    public class AppOptions
    {
        public const string ApiParameters = "ApiParameters";
        public string ApiKey { get; set; }
        public string Url { get; set; }
    }
}