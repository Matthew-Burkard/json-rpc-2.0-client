using System;

namespace JsonRpcClient.Exceptions
{
    public class RpcError : Exception
    {
        protected RpcError(string message) : base(message)
        {
        }
    }

    public class ParseError : RpcError
    {
        public ParseError() : base("Parse error")
        {
        }
    }

    public class InvalidRequest : RpcError
    {
        public InvalidRequest() : base("Invalid Request")
        {
        }
    }

    public class MethodNotFound : RpcError
    {
        public MethodNotFound() : base("Method not found")
        {
        }
    }

    public class InvalidParams : RpcError
    {
        public InvalidParams() : base("Invalid params")
        {
        }
    }

    public class InternalError : RpcError
    {
        public InternalError() : base("Internal error")
        {
        }
    }

    public class ServerError : RpcError
    {
        public ServerError(string message) : base(message)
        {
        }
    }
}