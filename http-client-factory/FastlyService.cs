using Microsoft.Extensions.Options;
using System.Net.Http;
using System.Threading.Tasks;

namespace http_client_factory
{
    public class FastlyService
    {
        private const string apiRoot = "https://api.fastly.com";

        private readonly IHttpClientFactory clientFactory;
        private readonly FastlyConfig settings;

        public FastlyService(IHttpClientFactory clientFactory, IOptions<FastlyConfig> options)
        {
            this.clientFactory = clientFactory;
            settings = options.Value;
        }

        public async Task<string> GetStats()
        {
            var response = await clientFactory
                .CreateClient()
                .SendAsync(Get("/stats"));

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }
            else
            {
                return $"StatusCode: {response.StatusCode}";
            }
        }

        private HttpRequestMessage Get(string path)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{apiRoot}{path}");
            request.Headers.Add("Fastly-Key", settings.ApiKey);
            return request;
        }
    }
}