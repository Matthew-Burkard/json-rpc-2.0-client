using System.Net.WebSockets;
using System.Text;

namespace JsonRpcClient.Clients;

public class RpcWsClient : RpcClient
{
    private readonly ClientWebSocket _client = new();
    private readonly string _baseUri;

    protected RpcWsClient(string baseUri)
    {
        _baseUri = baseUri;
    }

    public void SetRequestHeader(string headerName, string? headerValue)
    {
        _client.Options.SetRequestHeader(headerName, headerValue);
    }

    public async Task Connect()
    {
        await _client.ConnectAsync(new Uri(_baseUri), CancellationToken.None);
    }

    public async Task Close()
    {
        await _client.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closing the connection", CancellationToken.None);
    }

    protected override async Task<string> SendAndGetJson(string request)
    {
        var message = Encoding.UTF8.GetBytes(request);
        var buffer = new ArraySegment<byte>(message);
        await _client.SendAsync(buffer, WebSocketMessageType.Text, true, CancellationToken.None);

        var receiveBuffer = new ArraySegment<byte>(new byte[1024]);
        var response = "";
        WebSocketReceiveResult result;
        do
        {
            result = await _client.ReceiveAsync(receiveBuffer, CancellationToken.None);
            // Decode the response from the server and append it to the previous responses.
            response += Encoding.UTF8.GetString(receiveBuffer.Array ?? Array.Empty<byte>(), 0, result.Count);
        } while (!result.EndOfMessage);

        return response;
    }

    protected override async Task SendJson(string request)
    {
        var message = Encoding.UTF8.GetBytes(request);
        var buffer = new ArraySegment<byte>(message);
        await _client.SendAsync(buffer, WebSocketMessageType.Text, true, CancellationToken.None);
    }
}
