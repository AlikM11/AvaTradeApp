using AvaTradeApp.Infrastructure.Services.Interfaces;

namespace AvaTradeApp.Infrastructure.Services.Implementation
{
    public class HttpClientService : IHttpClientService, IDisposable
    {
        private readonly HttpClient _httpClient;
        public HttpClientService()
        {
            _httpClient = new HttpClient
            {
                Timeout = TimeSpan.FromSeconds(30)
            };

            _httpClient.DefaultRequestHeaders.Add("User-Agent", "CustomClient");
        }
        public async Task<HttpResponseMessage> GetAsync(string url, CancellationToken token)
        {
            return await _httpClient.GetAsync(url, token);
        }
        public async Task<HttpResponseMessage> SendRequestAsync(HttpRequestMessage request)
        {
            return await _httpClient.SendAsync(request);
        }
        public HttpClient GetHttpClient()
        {
            return _httpClient;
        }
        public void Dispose()
        {
            _httpClient.Dispose();
        }
    }
}
