using System.Threading.Tasks;
using JsonRpcClient.Clients;
using JsonRpcClient.Objects;
using Newtonsoft.Json;

namespace ClientTests
{
    public class RpcTestClient : RpcClient
    {
        private readonly TestServer _server = new();

        protected override async Task<string> SendRequest(RpcRequest request)
        {
            return _server.Methods[request.Method](JsonConvert.SerializeObject(request));
        }

        protected override async Task SendNotification(RpcRequest request)
        {
            _server.Methods[request.Method](JsonConvert.SerializeObject(request));
        }
    }
}