using System.Net;

namespace NetKuber.Middleware;

public class MiddlewareException:Exception
{
    public HttpStatusCode codigo { get; set; }
    public object? Error { get; set; }
    public MiddlewareException(HttpStatusCode codigo, object? Error=null)
    {
        this.codigo = codigo;
        this.Error = Error;
    }
    
}