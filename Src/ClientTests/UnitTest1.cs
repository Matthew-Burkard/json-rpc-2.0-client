using System.Collections.Generic;
using System.Threading.Tasks;
using JsonRpcClient.Clients;
using Newtonsoft.Json;
using NUnit.Framework;

namespace ClientTests
{
    public class Tests
    {
        private TestClient _client;

        [SetUp]
        public void Setup()
        {
            _client = new TestClient("http://localhost:8000/api/v1");
        }

        [Test]
        public async Task Test1()
        {
            Assert.AreEqual(await _client.GetStrings(), new List<string>());
        }
    }

    public class TestClient : RpcHttpClient
    {
        public TestClient(string baseUri) : base(baseUri)
        {
        }

        public async Task<List<string>> GetStrings()
        {
            var v = await Call("get_props");
            return JsonConvert.DeserializeObject<List<string>>(v.ToString()!);
        }
    }
}