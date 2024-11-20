namespace ProyectoTodoFrenosWeb.ConsumoServices
{
    public class HttpClientService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _config;
        public HttpClientService(IConfiguration config, IHttpClientFactory httpClientFactory)
        {
            _config = config;
            _httpClientFactory = httpClientFactory;
        }

        public HttpClient CreateClient()
        {
            var client = _httpClientFactory.CreateClient();  
            AddApiKeyHeader(client);
            return client;
        }

        public void AddApiKeyHeader(HttpClient client)
        {
             var apiKey = _config.GetValue<string>("ApiSettings:ApiKey");
            if (!string.IsNullOrEmpty(apiKey))
            {
                client.DefaultRequestHeaders.Add("ApiKey", apiKey);
            }
        }
    }
}
