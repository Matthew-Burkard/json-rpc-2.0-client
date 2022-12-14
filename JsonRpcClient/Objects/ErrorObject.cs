using Newtonsoft.Json;

namespace JsonRpcClient.Objects;

public class ErrorObject
{
    [JsonProperty("code", Required = Required.Always)]
    public int Code { get; set; }

    [JsonProperty("message", Required = Required.Always)]
    public string Message { get; set; } = null!;

    [JsonProperty("data")] public object? Data { get; set; }
}