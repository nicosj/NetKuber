using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetKuber.Models;

public class Productos
{
    [Key]
    [Required]
    public int Id { get; set; }
    public string Nombre { get; set;}
    public string Descripcion { get; set; }
    public string Imagen { get; set; }
    [Required]
    [Column(TypeName = "decimal(18,4)")]
    public int Precio { get; set; }
    public int Cantidad { get; set; }
    public int TiendaId { get; set; }
    public Tienda Tienda { get; set; }
}