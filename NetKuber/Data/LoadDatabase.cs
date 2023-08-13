using Microsoft.AspNetCore.Identity;
using NetKuber.Data;
namespace NetKuber.Models;


public class LoadDatabase
{
    public static async Task InsertarData(AppDbContext context, UserManager<Usuario> usuarioManager)
    {
        if(!usuarioManager.Users.Any())
        {
            var usuario = new Usuario
            {
                Nombre= "Admin",
                Apellido = "Admin",
                Email="admin@admin.com",
                UserName="nicosj",
                Telefono="123456789",
            };
            await usuarioManager.CreateAsync(usuario, "Abc123456789@");
        }

        if (!context.Inmuebles!.Any())
        {
            context.Inmuebles!.AddRange(
                new Inmueble
                {
                    Nombre = "Casa",
                    Direccion = "Calle 123",
                    Precio = 4500M,
                    FechaCreacion = DateTime.Now,
                },
                new Inmueble
                {
                    Nombre = "Departamento",
                    Direccion = "Calle 456",
                    Precio = 3500M,
                    FechaCreacion = DateTime.Now,
                }
            );
        }
        await context.SaveChangesAsync();
    }
}

