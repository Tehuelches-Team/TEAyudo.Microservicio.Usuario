using Application.Model.DTO;
using Domain.Entities;

namespace Application.Interface
{
    public interface IUsuarioCommand
    {
        Task<Usuario?> PostUsuario(Usuario Usuario);
        Task<Usuario> PutUsuario(int Id, UsuarioDTO Usuario);
        Task DeleteUsuario(int Id);
    }
}
