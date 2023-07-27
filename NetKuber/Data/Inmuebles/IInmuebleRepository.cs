using NetKuber.Models;

namespace NetKuber.Data.Inmuebles;

public interface IInmuebleRepository
{
    Task<bool> SaveChanges();
    Task<IEnumerable<Inmueble>> GetAllInmuebles();
    Task<Inmueble?> GetInmuebleById(int id);
    Task CreateInmueble(Inmueble inmueble);
    void UpdateInmueble(Inmueble inmueble);
    Task DeleteInmueble(Inmueble inmueble);
}