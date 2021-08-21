using System;
using JsonRpc2.Exceptions;
using JsonRpc2.JsonRpcObjects;
using Newtonsoft.Json;

namespace JsonRpc2.Clients
{
    public interface IRpcClient
    {
        const int ParseError = -32700;
        const int InvalidRequest = -32600;
        const int MethodNotFound = -32601;
        const int InvalidParams = -32602;
        const int InternalError = -32603;

        protected static string GenId()
        {
            return Guid.NewGuid().ToString();
        }

        protected static object HandleJson(string data)
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
    }
}