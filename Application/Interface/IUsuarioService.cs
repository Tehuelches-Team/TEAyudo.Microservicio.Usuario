using Application.Model.DTO;
using Application.Model.Response;

namespace Application.Interface
{
    public interface IUsuarioService
    {
        Task<List<UsuarioResponse>> GetAllUsuarios();
        Task<UsuarioResponse?> GetUsuarioById(int Id);
        Task<UsuarioResponse?> PostUsuario(UsuarioDTO UsuarioRecibido, string Token);
        Task<bool> ComprobarCorreo(string Correo);
        Task<UsuarioResponse?> PutUsuario(int Id, UsuarioDTO UsuarioRecibido);
        Task DeleteUsuario(int Id);
        Task<LogginResponse?> Loggin(string Correo,string contrasena);
    }
}
