using System.ComponentModel.DataAnnotations;

namespace NetKuber.Models;

public class Tienda
{
    [Key]
    [Required]
    public int Id { get; set; }
    
    public string? Nombre { get; set; }
    public string? Direccion { get; set; }
    public string? Telefono { get; set; }
    
    public string? Correo { get; set; }
    
    public string? Especialidad { get; set; }
    public string? Descripcion { get; set; }
    
    public string? Horario { get; set; }
    
    public string? Imagen { get; set; }
    public bool? Estado { get; set; }
    
    public string? Latitud { get; set; }
    public string? Longitud { get; set; }
    public string? Ciudad { get; set; }
    
    public float? stars { get; set; }
    public string? QRTienda{ get; set; }
    public string? QRUsuario { get; set; }
    public string? QRAfip { get; set; }
}