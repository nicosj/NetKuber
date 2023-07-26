using System.ComponentModel.DataAnnotations;
namespace NetKuber.Models;

public class Inmueble
{
    [key]
    [Required]
    public int Id { get; set; }
    public string ? Nombre { get; set; }
    public string? Direccion { get; set; }
    [Required]
    [Column(TypeName = "decimal(18,4)"))]
    public decimal  Precio { get; set; }
    public string? Picture  { get; set; }
    public DateTime? FechaCreacion { get; set; }
    public Guid? UsuarioId { get; set; }
}