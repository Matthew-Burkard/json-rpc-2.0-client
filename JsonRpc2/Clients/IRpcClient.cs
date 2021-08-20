using System;
using JsonRpc2.JsonRpcObjects;
using Newtonsoft.Json;

namespace JsonRpc2.Clients
{
    public interface IRpcClient
    {
        protected static string GenId()
        {
            return Guid.NewGuid().ToString();
        }

        protected static object HandleJson(string data)
        {
            var resp = JsonConvert.DeserializeObject<RpcResponse>(data);
            if (resp is {Error: { }})
            {
                throw new Exception("TODO Rpc Exceptions");
            }

            if (resp != null)
            {
                return resp.Result;
            }

            throw new Exception("TODO Rpc Exceptions");
        }
    }
}