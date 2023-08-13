using Microsoft.AspNetCore.Identity;
namespace NetKuber.Models;


public class Usuario : IdentityUser
{
    public string? Nombre { get; set; }
    public string? UserName { get; set; }
    public string? Apellido { get; set; }
    public string? Email { get; set; }
    public string? Telefono { get; set; }
    
    public Guid UsuarioId { get; set; }
    

}