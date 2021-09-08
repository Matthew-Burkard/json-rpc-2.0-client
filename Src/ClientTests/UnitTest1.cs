using System.Collections.Generic;
using System.Threading.Tasks;
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
            _client = new TestClient();
        }

        [Test]
        public async Task TestRequest()
        {
            Assert.AreEqual(new List<string> { "TestRequest" }, await _client.TestRequest());
        }

        [Test]
        public async Task TestParamsRequest()
        {
            var testParams = new List<string> { "test1", "test2", "test3" };
            Assert.AreEqual(testParams, await _client.TestParamsRequest(testParams));
        }

        [Test]
        public async Task TestNotification()
        {
            await _client.TestNotification();
            Assert.Pass();
        }

        [Test]
        public async Task TestParamsNotification()
        {
            await _client.TestParamsNotification(new List<string>());
            Assert.Pass();
        }
    }

    public class TestClient : RpcTestClient
    {
        public async Task<List<string>> TestRequest()
        {
            var v = await SendRequest("test_request");
            return JsonConvert.DeserializeObject<List<string>>(v.ToString()!);
        }

        public async Task<List<string>> TestParamsRequest(List<string> strings)
        {
            var v = await SendRequest("test_params_request", strings);
            return JsonConvert.DeserializeObject<List<string>>(v.ToString()!);
        }

        public async Task TestNotification()
        {
            await SendNotification("test_notification");
        }

        public async Task TestParamsNotification(List<string> strings)
        {
            await SendNotification("test_params_notification", strings);
        }
    }
}