using System.Net;
using System.Security.Cryptography.Xml;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NetKuber.Dtos.UsuarioDtos;
using NetKuber.Middleware;
using NetKuber.Models;
using NetKuber.Token;

namespace NetKuber.Data.Usuarios;

public class UsuarioRepository: IUsuarioRepository
{
    private readonly UserManager<Usuario> _userManager;
    private readonly SignInManager<Usuario> _signInManager; 
    private readonly IJwtGenerador _jwtGenerador;
    private readonly AppDbContext _context;
    private readonly IUsuarioSesion _usuarioSesion;

    public UsuarioRepository(
        UserManager<Usuario> userManager,
        SignInManager<Usuario> signInManager,
        IJwtGenerador jwtGenerador,
        AppDbContext context,
        IUsuarioSesion usuarioSesion)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _jwtGenerador = jwtGenerador;
        _context = context;
        _usuarioSesion = usuarioSesion;
        
    }
    
    public async Task<UsuarioResponseDto> GetUsuario()
    {
      var usuario = await _userManager.FindByIdAsync(_usuarioSesion.ObtenerUsuarioSesion());
      if (usuario is null)
      {
          throw new MiddlewareException(HttpStatusCode.Unauthorized,new {mensaje="El usuario del token no existe en la base de datos"});
      }
      return TransformerUserToUserDto(usuario!);
    }

    private UsuarioResponseDto TransformerUserToUserDto(Usuario usuario)
    {
        return new UsuarioResponseDto
        {
            id=usuario.Id,
            Nombre = usuario.Nombre,
            Apellido = usuario.Apellido,
            Email = usuario.Email,
            UserName = usuario.UserName,
            Telefono = usuario.Telefono,
            Token= _jwtGenerador.CrearToken(usuario)
            
        };
    }

    public async Task<UsuarioResponseDto> Login(UsuarioLoginRequestDto request)
    {
        var usuariod = await _userManager.FindByEmailAsync(request.Email!);
        if (usuariod is null)
        {
            throw new MiddlewareException(HttpStatusCode.Unauthorized,new {mensaje="El usuario no existe en la base de datos"});
        }
        var result = await _signInManager.CheckPasswordSignInAsync(usuariod!, request.Password!, false);
        if (result.Succeeded)
        {
            return TransformerUserToUserDto(usuariod);
        }
        throw new MiddlewareException(HttpStatusCode.Unauthorized,new {mensaje="El usuario o la contraseña son incorrectos"});
    }

    public async Task<UsuarioResponseDto> RegistroUsuario(UsuarioRegistroRequestDto request)
    {
        var existe = await _context.Users.Where(x => x.Email == request.Email).AnyAsync();
        var existeUserName = await _context.Users.Where(x => x.UserName == request.UserName).AnyAsync();
        if (existe && existeUserName)
        {
            throw new MiddlewareException(HttpStatusCode.BadRequest,new {mensaje="El email o username ya existe en la base de datos"});
        }
        var usuario = new Usuario
        {
            Nombre = request.Nombre,
            Apellido = request.Apellido,
            Email = request.Email,
            UserName = request.UserName,
            Telefono = request.Telefono
        };
        var resul=await _userManager.CreateAsync(usuario, request.Password!);
        if (resul.Succeeded)
        {
            return TransformerUserToUserDto(usuario);    
        }
        throw new MiddlewareException(HttpStatusCode.BadRequest,new {mensaje="No se pudo crear el usuario"});
        
    }
}