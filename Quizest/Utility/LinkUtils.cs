namespace Utility
{
    public static class LinkUtils
    {
        public static string GenerateTemporaryLink(bool isHttps, string host, string link, string queryParam)
        {
            string scheme = isHttps ? "https" : "http";
            return $"{scheme}://{host}{link}/generated/{queryParam}";
        }
    }
}
