using System;
using System.Collections.Generic;
using JsonRpcClient.Objects;
using Newtonsoft.Json;

namespace ClientTests
{
    public class TestServer
    {
        public Dictionary<string, Func<string, string>> Methods { get; }

        public TestServer()
        {
            Methods = new Dictionary<string, Func<string, string>>
            {
                ["test_request"] = TestRequest,
                ["test_params_request"] = TestParamsRequest,
                ["test_notification"] = TestNotification,
                ["test_params_notification"] = TestParamsNotification
            };
        }

        private static string TestRequest(string parameters)
        {
            var request = JsonConvert.DeserializeObject<RequestObject>(parameters);
            Console.WriteLine("TestRequest");
            var resp = new ResponseObject {
                Id = request?.Id,
                Result = new List<string> { "TestRequest" }
            };
            return JsonConvert.SerializeObject(resp);
        }

        private static string TestParamsRequest(string parameters)
        {
            var request = JsonConvert.DeserializeObject<RequestObject>(parameters);
            Console.WriteLine("TestParamsRequest");
            var resp = new ResponseObject {
                Id = request?.Id,
                Result = request?.Params
            };
            return JsonConvert.SerializeObject(resp);
        }

        private static string TestNotification(string parameters)
        {
            return "TestNotification";
        }

        private static string TestParamsNotification(string parameters)
        {
            return "TestParamsNotification";
        }
    }
}