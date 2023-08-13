using System.ComponentModel.DataAnnotations;

namespace NetKuber.Models;

public class Especialista
{
    [Key]
    [Required]
    public int Id { get; set; }
    public string? Nombre { get; set; }
    public string? Estado { get; set; }
    public string? Cidade { get; set; }
    public string? Bairro { get; set; }
    public string? Numero { get; set; }
    public float? stars { get; set; }
    public int ? CategoriaId{ get; set; }
    
}