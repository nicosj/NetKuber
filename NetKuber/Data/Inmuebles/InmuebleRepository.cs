using System.Net;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NetKuber.Middleware;
using NetKuber.Models;
using NetKuber.Token;

namespace NetKuber.Data.Inmuebles;

public class InmuebleRepository:IInmuebleRepository
{
    private readonly AppDbContext _context;
    private readonly IUsuarioSesion _usuarioSesion;
    private readonly UserManager<Usuario> _userManager;
    public InmuebleRepository(
        AppDbContext context, 
        IUsuarioSesion usuarioSesion,
        UserManager<Usuario> userManager)
    {
        _context = context;
        _usuarioSesion = usuarioSesion;
        _userManager = userManager;
    }
    
    public async Task<bool> SaveChanges()
    {
        return  ((await _context.SaveChangesAsync()) >= 0);
    }

    public async  Task<IEnumerable<Inmueble>> GetAllInmuebles()
    {
        return await _context.Inmuebles!.ToListAsync();
    }

    public async Task<Inmueble?> GetInmuebleById(int id)
    {
        return await _context.Inmuebles!.FirstOrDefaultAsync(x=>x.Id == id)!;
    }

    public async Task CreateInmueble(Inmueble inmueble)
    {
        var usuario = await _userManager.FindByNameAsync(_usuarioSesion.ObtenerUsuarioSesion());
        if (usuario is null)
        {
            throw new MiddlewareException(HttpStatusCode.Unauthorized,new {mensaje="El usuario no es valido  en la base de datos"});
        }

        if (inmueble is null)
        {
            throw new MiddlewareException(HttpStatusCode.BadRequest, new { mensaje = "El inmueble no es valido" });
        }
        inmueble.FechaCreacion = DateTime.Now;
        inmueble.UsuarioId= Guid.Parse(usuario.Id);
        await _context.Inmuebles!.AddAsync(inmueble);
    }

    public void UpdateInmueble(Inmueble inmueble)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteInmueble(Inmueble inmueble)
    {
        var inmuebleBorrar =await _context.Inmuebles!.FirstOrDefaultAsync(x=>x.Id == inmueble.Id);
        _context.Inmuebles!.Remove(inmuebleBorrar);
    }
}