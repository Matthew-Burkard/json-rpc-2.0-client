# JSON RPC 2.0 Client

Provides classes for creating JSON RPC 2.0 clients in C#.
By default this supports HTTP with `RpcHttpClient` and WebSockets with `RpcWsClient`.
Support for other transport protocols can be added by writing a class that extends `RpcClient`.

## Usage Example

Supposing the JSON RPC server defines the methods "add", "subtract", and "divide", expecting requests like this:

```json
{
  "id": 1,
  "method": "add",
  "params": [2, 3],
  "jsonrpc": "2.0"
}

{
  "id": 2,
  "method": "subtract",
  "params": [2, 3],
  "jsonrpc": "2.0"
}

{
  "id": 3,
  "method": "divide",
  "params": [3, 2],
  "jsonrpc": "2.0"
}
```

Defining and using the corresponding client would look like this:

```c#
using JsonRpcClient.Clients;

public class MathClient : RpcHttpClient
{
    public MathClient(string baseUri) : base(baseUri)
    {
    }

    public async Task<long> Add(int a, int b)
    {
        return (long) (await Call("add", new List<int> {a, b}) ?? throw new InvalidOperationException());
    }

    public async Task<long> Subtract(int a, int b)
    {
        return (long) (await Call("subtract", new List<int> {a, b}) ?? throw new InvalidOperationException());
    }

    public async Task<double> Divide(int a, int b)
    {
        return (double) (await Call("divide", new List<int> {a, b}) ?? throw new InvalidOperationException());
    }
}
```

This client will form request bodies, send them to the server and return the result.

## Errors

If the server responds with an error, an RpcError is thrown.
There is an RpcError for each standard JSON RPC 2.0 error, each of them extends RpcError.

```c#
var client = new MathClient("http://localhost:5000/api/v1");

try
{
    client.add("two", "three");
}
catch (InvalidParams e)
{
    Console.WriteLine(e);
}

try
{
    client.divide(0, 0);
}
catch (ServerError e)
{
    Console.WriteLine(e);
}
```
