using Microsoft.AspNetCore.Identity;
using NetKubernates.DTO.Usuario;
using NetKubernates.Models;
using NetKubernates.Token;

namespace NetKubernates.Data.Usuarios;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly UserManager<Usuario> _userManager;
    private readonly SignInManager<Usuario> _signInManager;
    private readonly IJwtGenerator _jwtGenerator;
    private readonly AppDbContext _appDbContext;
    private readonly IUserSession _userSession;

    public UsuarioRepository(
        UserManager<Usuario> userManager,
        SignInManager<Usuario> signInManager,
        IJwtGenerator jwtGenerator,
        AppDbContext appDbContext,
        IUserSession userSession)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _jwtGenerator = jwtGenerator;
        _appDbContext = appDbContext;
        _userSession = userSession;
    }

    private UsuarioResponseDTO TransformUserToUserDTO(Usuario user){
        return new UsuarioResponseDTO
        {
            Id = user.Id,
            Name = user.Name,
            LastName = user.LastName,
            Email = user.Email,
            UserName = user.UserName,
            Token = _jwtGenerator.GenerateToken(user)
        };
    }


    public async Task<UsuarioResponseDTO> GetUsuario()
    {
        var user = await _userManager.FindByNameAsync(_userSession.getUserSession());

        return TransformUserToUserDTO(user!);
    }

    public async Task<UsuarioResponseDTO> Login(UsuarioLoginRequestDTO request)
    {
        var user = await _userManager.FindByEmailAsync(request.Email!);

        await _signInManager.CheckPasswordSignInAsync(user!, request.Password!, false);

        return TransformUserToUserDTO(user!);
    }

    public async Task<UsuarioResponseDTO> Register(UsuarioRegistroRequestDTO request)
    {
        var newUser = new Usuario{
            Name = request.Name,
            Email = request.Email,
            UserName = request.UserName,
            Phone = request.Phone,
        };
        
        await _userManager.CreateAsync(newUser, request.Password!);

        return TransformUserToUserDTO(newUser);
    }
}