using Application.Model.DTO;
using Application.Model.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface
{
    public interface IUsuarioService
    {
        Task<List<UsuarioResponse>> GetAllUsuarios();
        Task<UsuarioResponse?> GetUsuarioById(int Id);
        Task<UsuarioResponse?> PostUsuario(UsuarioDTO UsuarioRecibido, string Token);
        Task<bool> ComprobarCorreo(string Correo);
        Task<UsuarioResponse?> PutUsuario(int Id, UsuarioDTO UsuarioRecibido); 
    }
}
