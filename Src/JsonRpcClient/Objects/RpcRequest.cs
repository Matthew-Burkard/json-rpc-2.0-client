using Newtonsoft.Json;

namespace JsonRpcClient.Objects
{
    public class RpcRequest
    {
        [JsonProperty("id", Required = Required.Always)]
        public object Id { get; set; }
        [JsonProperty("jsonrpc", Required = Required.Always)]
        public string JsonRpc { get; set; } = "2.0";
        [JsonProperty("method", Required = Required.Always)]
        public string Method { get; set; }
        [JsonProperty("params")]
        public object Params { get; set; }
    }
}