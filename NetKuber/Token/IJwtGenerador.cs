using NetKuber.Models;
namespace NetKuber.Token;

public interface IJwtGenerador
{
    string CrearToken(Usuario usuario);
}


