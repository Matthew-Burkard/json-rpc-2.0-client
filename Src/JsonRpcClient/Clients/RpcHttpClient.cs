using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using JsonRpcClient.Objects;
using Newtonsoft.Json;

namespace JsonRpcClient.Clients
{
    public abstract class RpcHttpClient : RpcClient
    {
        private readonly HttpClient _client = new();

        protected RpcHttpClient(string baseUri)
        {
            _client.BaseAddress = new Uri(baseUri);
            _client.DefaultRequestHeaders
                .Accept
                .Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        protected override async Task<string> SendRequest(RpcRequest request)
        {
            var response = await _client.PostAsync(
                "",
                new StringContent(
                    JsonConvert.SerializeObject(request),
                    Encoding.UTF8,
                    "application/json"
                )
            );
            return await response.Content.ReadAsStringAsync();
        }

        protected override async Task SendNotification(RpcRequest request)
        {
            await _client.PostAsync(
                "",
                new StringContent(
                    JsonConvert.SerializeObject(request),
                    Encoding.UTF8,
                    "application/json"
                )
            );
        }
    }
}