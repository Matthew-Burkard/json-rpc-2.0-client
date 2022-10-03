using JsonRpcClient.Objects;

namespace JsonRpcClient.Exceptions;

public class RpcError : Exception
{
    protected static readonly ErrorObject ParseErrorObject = new() {Code = 0, Message = "Parse error"};
    protected static readonly ErrorObject InvalidRequestObject = new() {Code = 0, Message = "Invalid request"};
    protected static readonly ErrorObject MethodNotFoundObject = new() {Code = 0, Message = "Method not found"};
    protected static readonly ErrorObject InvalidParamsObject = new() {Code = 0, Message = "Invalid params"};
    protected static readonly ErrorObject InternalErrorObject = new() {Code = 0, Message = "Internal error"};

    protected RpcError(ErrorObject error)
        : base($"{error.Code}: {error.Message}{($"\n{error.Data}")}")
    {
    }
}

public class ParseError : RpcError
{
    public ParseError(ErrorObject? error = null) : base(error ?? ParseErrorObject)
    {
    }
}

public class InvalidRequest : RpcError
{
    public InvalidRequest(ErrorObject? error = null) : base(error ?? InvalidRequestObject)
    {
    }
}

public class MethodNotFound : RpcError
{
    public MethodNotFound(ErrorObject? error = null) : base(error ?? MethodNotFoundObject)
    {
    }
}

public class InvalidParams : RpcError
{
    public InvalidParams(ErrorObject? error = null) : base(error ?? InvalidParamsObject)
    {
    }
}

public class InternalError : RpcError
{
    public InternalError(ErrorObject? error = null) : base(error ?? InternalErrorObject)
    {
    }
}

public class ServerError : RpcError
{
    public ServerError(ErrorObject error) : base(error)
    {
    }
}