using NetKuber.Dtos.UsuarioDtos;
using NetKuber.Models;

namespace NetKuber.Data.Usuarios;

public interface IUsuarioRepository
{
    Task<UsuarioResponseDto> GetUsuario();
    Task<UsuarioResponseDto> Login(UsuarioLoginRequestDto request);
    Task<UsuarioResponseDto> RegistroUsuario(UsuarioRegistroRequestDto request);
    
}