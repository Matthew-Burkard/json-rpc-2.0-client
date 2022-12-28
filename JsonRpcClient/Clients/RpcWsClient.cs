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

        var receivedBuffer = new ArraySegment<byte>(new byte[1024]);
        var result = await _client.ReceiveAsync(receivedBuffer, CancellationToken.None);
        return receivedBuffer.Array != null
            ? Encoding.UTF8.GetString(receivedBuffer.Array, 0, result.Count)
            : "";
    }

    protected override async Task SendJson(string request)
    {
        var message = Encoding.UTF8.GetBytes(request);
        var buffer = new ArraySegment<byte>(message);
        await _client.SendAsync(buffer, WebSocketMessageType.Text, true, CancellationToken.None);
    }
}
