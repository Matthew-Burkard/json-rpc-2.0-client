using JsonRpcClient.Exceptions;
using JsonRpcClient.Objects;
using Newtonsoft.Json;

namespace JsonRpcClient.Clients;

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
    protected async Task<object?> Call(string method, bool isNotification = false)
    {
        var request = new RequestObject {Method = method};
        if (!isNotification)
        {
            request.Id = GenId();
            return HandleJson(await SendAndGetJson(JsonConvert.SerializeObject(request)));
        }

        await SendAndGetJson(JsonConvert.SerializeObject(request));
        return null;
    }

    /*
     * Call an RPC method and get a response.
     */
    protected async Task<object?> Call(string method, object parameters, bool isNotification = false)
    {
        var request = new RequestObject {Method = method, Params = parameters};
        if (!isNotification)
        {
            request.Id = GenId();
            return HandleJson(await SendAndGetJson(JsonConvert.SerializeObject(request)));
        }

        await SendAndGetJson(JsonConvert.SerializeObject(request));
        return null;
    }

    /*
     * Override with method to send a request and return the
     * response content.
     */
    protected abstract Task<string> SendAndGetJson(string request);

    private static object? HandleJson(string data)
    {
        var resp = JsonConvert.DeserializeObject<ResponseObject>(data);
        if (resp is {Error: { }})
        {
            throw resp.Error.Code switch
            {
                ParseError => new ParseError(resp.Error),
                InvalidRequest => new InvalidRequest(resp.Error),
                MethodNotFound => new MethodNotFound(resp.Error),
                InvalidParams => new InvalidParams(resp.Error),
                InternalError => new InternalError(resp.Error),
                _ => new ServerError(resp.Error)
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