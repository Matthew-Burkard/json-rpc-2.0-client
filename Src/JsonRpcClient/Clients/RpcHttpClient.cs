using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace JsonRpcClient.Clients
{
    public abstract class RpcHttpClient : RpcClient
    {
        private readonly HttpClient _client = new();

        protected RpcHttpClient(string baseUri)
        {
            _client.BaseAddress = new Uri(baseUri);
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        protected override async Task<string> SendAndGetJson(string request)
        {
            var response = await _client.PostAsync("", new StringContent(request, Encoding.UTF8, "application/json"));
            return await response.Content.ReadAsStringAsync();
        }
    }
}