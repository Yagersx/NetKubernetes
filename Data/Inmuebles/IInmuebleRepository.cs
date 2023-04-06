using NetKubernates.Models;
namespace NetKubernates.Data.Inmuebles;


public interface IInmuebleRepository{
    bool SaveChanges();

    IEnumerable<Inmueble> GetAllInmuebles();

    Inmueble GetInmuebleById(int id);

    Task CreateInmueble(Inmueble inmueble);

    void DeleteInmueble(int id);

    void UpdateInmueble(Inmueble inmueble);
}