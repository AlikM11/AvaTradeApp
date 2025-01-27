namespace AvaTradeApp.Infrastructure.Services.Interfaces
{
    /// <summary>
    /// The IHttpClientService interface defines methods for sending HTTP requests, specifically for sending general HTTP requests, performing GET requests with cancellation support, and obtaining an instance of HttpClient.
    /// </summary>
    public interface IHttpClientService
    {
        public HttpClient GetHttpClient();
        public Task<HttpResponseMessage> SendRequestAsync(HttpRequestMessage request);
        public Task<HttpResponseMessage> GetAsync(string url, CancellationToken token);
    }
}
