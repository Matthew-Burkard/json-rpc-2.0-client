using Newtonsoft.Json;

namespace JsonRpcClient.Objects;

public class ResponseObject
{
    [JsonProperty("id")] public object? Id { get; set; }

    [JsonProperty("jsonrpc", Required = Required.Always)]
    public string JsonRpc { get; set; } = "2.0";

    [JsonProperty("result")] public object? Result { get; set; }
    [JsonProperty("error")] public ErrorObject? Error { get; set; }
}