using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using NetKuber.Models;
using Microsoft.EntityFrameworkCore;
namespace NetKuber.Data;
public class AppDbContext:IdentityDbContext<Usuario>
{
    public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }
    
    public DbSet<Inmueble>? Inmuebles { get; set; }
    
}

