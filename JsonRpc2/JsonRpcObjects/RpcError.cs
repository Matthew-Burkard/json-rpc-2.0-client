using Newtonsoft.Json;

namespace JsonRpc2.JsonRpcObjects
{
    public class RpcError
    {
        [JsonProperty("code", Required = Required.Always)]
        public int Code { get; set; }
        [JsonProperty("message", Required = Required.Always)]
        public string Message { get; set; }
        [JsonProperty("data")]
        public object Data { get; set; }
    }
}