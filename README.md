# JSON RPC 2.0 Client

Provides the classes needed to create JSON RPC 2.0 clients in C#.

## Usage

Supposing the JSON RPC server defines the methods "add", "subtract", and "divide", expecting requests like this:

```json
[
  {
    "id": 1,
    "method": "add",
    "params": [2, 3],
    "jsonrpc": "2.0"
  },
  {
    "id": 2,
    "method": "subtract",
    "params": [2, 3],
    "jsonrpc": "2.0"
  },
  {
    "id": 3,
    "method": "divide",
    "params": [3, 2],
    "jsonrpc": "2.0"
  }
]
```

Defining and using the corresponding client would look like this:

```c#
using System.Collections.Generic;
using System.Threading.Tasks;
using JsonRpcClient.Clients;
using Newtonsoft.Json;

namespace MathJsonRpcClient
{
    public class MathClientDriver()
    {
        public void main(string[] args)
        {
            var client = MathClient("http://localhost:5000/api/v1");
            client.add(2, 3); // 5
            client.subtract(2, 3); // -1
            client.divide(3, 2); // 1.5
        }
    }

    public class MathClient : RpcHttpClient
    {
        public async Task<int> Add(int a, int b)
        {
            var v = await Request("add", new List<int>{a, b});
            return JsonConvert.DeserializeObject<int>(v.ToString());
        }

        public async Task<int> Subtract(int a, int b)
        {
            var v = await Request("subtract", new List<int>{a, b});
            return JsonConvert.DeserializeObject<int>(v.ToString());
        }

        public async Task<int> Divide(int a, int b)
        {
            var v = await Request("divide", new List<int>{a, b});
            return JsonConvert.DeserializeObject<float>(v.ToString());
        }
    }
}
```

## Errors

If the server responds with an error an RpcError. There is an RpcError for each standard JSON RPC 2.0 error, each of
them extends RpcError.

```c#
public void main(string[] args)
{
    var client = MathClient("http://localhost:5000/api/v1");
    try
    {
        client.multiply(2, 3);
    }
    catch (MethodNotFound e)
    {
        Console.WriteLine(e);
    }
    try
    {
        client.add("two", "three");
    }
    catch (InvalidParams e)
    {
        Console.WriteLine(e);
    }
}
```
