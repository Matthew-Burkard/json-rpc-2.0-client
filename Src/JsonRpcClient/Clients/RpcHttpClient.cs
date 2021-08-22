using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using JsonRpcClient.Objects;
using Newtonsoft.Json;

namespace JsonRpcClient.Clients
{
    public abstract class RpcHttpClient : IRpcClient
    {
        private readonly HttpClient _client = new();

        protected RpcHttpClient(string baseUri)
        {
            _client.BaseAddress = new Uri(baseUri);
            _client.DefaultRequestHeaders
                .Accept
                .Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        protected async Task<object> Call(string method)
        {
            var request = new RpcRequest
            {
                Id = IRpcClient.GenId(),
                Method = method
            };
            return await Call(request);
        }

        protected async Task<object> Call(string method, object parameters)
        {
            var request = new RpcRequest
            {
                Id = IRpcClient.GenId(),
                Method = method,
                Params = parameters
            };
            return await Call(request);
        }

        private async Task<object> Call(RpcRequest request)
        {
            var response = await _client.PostAsync(
                "",
                new StringContent(
                    JsonConvert.SerializeObject(request),
                    Encoding.UTF8,
                    "application/json"
                )
            );
            var json = await response.Content.ReadAsStringAsync();
            return IRpcClient.HandleJson(json);
        }
    }
}