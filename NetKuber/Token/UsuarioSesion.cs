namespace NetKuber.Token;
using System.Security.Claims;

public class UsuarioSesion : IUsuarioSesion
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    public UsuarioSesion(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }
    public string ObtenerUsuarioSesion()
    {
     var userName=_httpContextAccessor.HttpContext.User?.Claims?.FirstOrDefault(x=>x.Type==ClaimTypes.NameIdentifier)?.Value;
     return userName;
    }
}
