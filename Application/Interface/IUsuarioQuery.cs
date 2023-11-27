using Domain.Entities;

namespace Application.Interface
{
    public interface IUsuarioQuery
    {
        Task<List<Usuario>> GetAllUsuarios();
        Task<Usuario?> GetUsuarioById(int Id);
        Task<bool> GetUsuarioByEmail(string Correo);
        Task<Usuario> GetUsuarioByLoggin(string Correo,string contrasena);
    }
}
