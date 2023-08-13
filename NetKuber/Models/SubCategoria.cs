namespace NetKuber.Models;

public class SubCategoria
{
    public int Id { get; set; }
    public string? Nombre { get; set; }
    public int CategoriaId { get; set; }
}