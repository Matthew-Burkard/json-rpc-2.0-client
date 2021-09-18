using System.Threading.Tasks;
using JsonRpcClient.Clients;
using JsonRpcClient.Exceptions;
using JsonRpcClient.Objects;
using Newtonsoft.Json;

namespace ClientTests
{
    public class RpcTestClient : RpcClient
    {
        private readonly TestServer _server = new();

        protected override async Task<string> SendAndGetJson(string request)
        {
            var requestObject = JsonConvert.DeserializeObject<RequestObject>(request) ?? throw new InternalError();
            return _server.Methods[requestObject.Method](request);
        }
    }
}