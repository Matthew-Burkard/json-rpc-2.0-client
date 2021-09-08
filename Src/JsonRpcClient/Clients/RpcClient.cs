using System;
using System.Threading.Tasks;
using JsonRpcClient.Exceptions;
using JsonRpcClient.Objects;
using Newtonsoft.Json;

namespace JsonRpcClient.Clients
{
    // TODO Batching
    public abstract class RpcClient
    {
        private const int ParseError = -32700;
        private const int InvalidRequest = -32600;
        private const int MethodNotFound = -32601;
        private const int InvalidParams = -32602;
        private const int InternalError = -32603;

        /*
         * Call an RPC method and get a response.
         */
        protected async Task<object> SendRequest(string method, object parameters)
        {
            var request = new RpcRequest
            {
                Id = GenId(),
                Method = method,
                Params = parameters
            };
            return HandleJson(await SendRequest(request));
        }

        /*
         * Call an RPC method and get a response.
         */
        protected async Task<object> SendRequest(string method)
        {
            var request = new RpcRequest
            {
                Id = GenId(),
                Method = method
            };
            return HandleJson(await SendRequest(request));
        }

        /*
         * Call an RPC method.
         */
        protected async Task SendNotification(string method, object parameters)
        {
            var request = new RpcRequest
            {
                Method = method,
                Params = parameters
            };
            await SendNotification(request);
        }

        /*
         * Call an RPC method.
         */
        protected async Task SendNotification(string method)
        {
            var request = new RpcRequest { Method = method };
            await SendNotification(request);
        }

        /*
         * Override with method to send a request and return the
         * response content.
         */
        protected abstract Task<string> SendRequest(RpcRequest request);

        /*
         * Override with method to send a request.
         */
        protected abstract Task SendNotification(RpcRequest request);

        private static object HandleJson(string data)
        {
            var resp = JsonConvert.DeserializeObject<RpcResponse>(data);
            if (resp is { Error: { } })
            {
                throw resp.Error.Code switch
                {
                    ParseError => new ParseError(),
                    InvalidRequest => new InvalidRequest(),
                    MethodNotFound => new MethodNotFound(),
                    InvalidParams => new InvalidParams(),
                    InternalError => new InternalError(),
                    _ => new ServerError(resp.Error.Message)
                };
            }

            if (resp != null)
            {
                return resp.Result;
            }

            throw new InternalError();
        }

        private static string GenId()
        {
            return Guid.NewGuid().ToString();
        }
    }
}