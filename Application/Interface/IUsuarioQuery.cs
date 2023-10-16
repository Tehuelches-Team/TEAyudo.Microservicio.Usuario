using Application.Model.Response;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface
{
    public interface IUsuarioQuery
    {
        Task<List<Usuario>> GetAllUsuarios();
        Task<Usuario?> GetUsuarioById(int Id);
        Task<bool> GetUsuarioByEmail(string Correo);
    }
}
