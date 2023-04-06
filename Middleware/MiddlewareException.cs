using System.Net;

namespace NetKubernates.Middleware;

public class MiddlewareException : Exception{

    public HttpStatusCode Code { get; set; }
    public object? Error { get; set; }

    public MiddlewareException(HttpStatusCode code, object? error = null) 
    {
        Code = code;
        Error = error;
    }

}