using Newtonsoft.Json;

namespace JsonRpcClient.Objects;

public class RequestObject
{
    [JsonProperty("id")] public object? Id { get; set; }

    [JsonProperty("jsonrpc", Required = Required.Always)]
    public string JsonRpc { get; set; } = "2.0";

    [JsonProperty("method", Required = Required.Always)]
    public string Method { get; set; } = null!;

    [JsonProperty("params")] public object? Params { get; set; }
}