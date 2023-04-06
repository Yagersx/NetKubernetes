using Microsoft.AspNetCore.Identity;
using NetKubernates.Models;
using NetKubernates.Token;

namespace NetKubernates.Data.Inmuebles;

public class InmuebleRepository : IInmuebleRepository
{
    private readonly AppDbContext _context;
    private readonly IUserSession _userSession;

    private readonly UserManager<Usuario> _userManager;

    public InmuebleRepository(
        AppDbContext context,
        IUserSession userSession,
        UserManager<Usuario> userManager
        )
    {
        _context = context;
        _userSession = userSession;
        _userManager = userManager;

    }

    public async Task CreateInmueble(Inmueble inmueble)
    {

        var user = await _userManager.FindByNameAsync(_userSession.getUserSession());

        inmueble.CreationDate = DateTime.Now;
        inmueble.UserId =Guid.Parse(user!.Id);

        _context.Inmuebles!.Add(inmueble);
    }

    public void DeleteInmueble(int id)
    {
        var inmueble = _context.Inmuebles!.FirstOrDefault(inmueble => inmueble.Id == id);

        _context.Inmuebles!.Remove(inmueble!);
    }

    public IEnumerable<Inmueble> GetAllInmuebles()
    {
        return _context.Inmuebles!.ToList().OrderDescending();
    }

    public Inmueble GetInmuebleById(int id)
    {
        return _context.Inmuebles!.FirstOrDefault(inmueble => inmueble.Id == id)!;
    }

    public bool SaveChanges()
    {
        return _context.SaveChanges() >=0 ? true : false;
    }

    public void UpdateInmueble(Inmueble inmueble)
    {
        throw new NotImplementedException();
    }
}