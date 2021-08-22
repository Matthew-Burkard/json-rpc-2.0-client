using Newtonsoft.Json;

namespace JsonRpcClient.Objects
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