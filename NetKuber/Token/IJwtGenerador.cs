using NetKuber.Models;
namespace NetKuber.Token;

public interface IJwGenerador
{
    string CrearToken(Usuario usuario);
}


