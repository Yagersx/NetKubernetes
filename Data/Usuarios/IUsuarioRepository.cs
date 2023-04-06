using NetKubernates.DTO.Usuario;

namespace NetKubernates.Data.Usuarios;

public interface IUsuarioRepository{
    Task<UsuarioResponseDTO> GetUsuario();

    Task<UsuarioResponseDTO> Login(UsuarioLoginRequestDTO request);

    Task<UsuarioResponseDTO> Register(UsuarioRegistroRequestDTO request);
}