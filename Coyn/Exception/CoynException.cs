using System.Net;

namespace Coyn.Exception;

public class CoynException: System.Exception
{
    public readonly HttpStatusCode StatusCode;
    
    public CoynException(string message): base(message)
    {
        this.StatusCode = HttpStatusCode.InternalServerError;
    }

    public CoynException(HttpStatusCode statusCode, string message): base(message)
    {
        StatusCode = statusCode;
    }
}