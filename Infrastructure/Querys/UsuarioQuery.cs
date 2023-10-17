using Application.Interface;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Querys
{
    public class UsuarioQuery : IUsuarioQuery
    {
        private readonly TEAyudoContext Context;
        public UsuarioQuery(TEAyudoContext Context)
        {
            this.Context = Context;
        }

        public async Task<List<Usuario>> GetAllUsuarios()
        {
            List<Usuario> ListaUsuario = await Context.Usuarios.ToListAsync();
            return ListaUsuario;
        }

        public async Task<bool> GetUsuarioByEmail(string Correo)
        {
            if (await Context.Usuarios.FirstOrDefaultAsync(s => s.CorreoElectronico == Correo) == null)
                return false;
            return true;
        }

        public async Task<Usuario?> GetUsuarioById(int Id)
        {
            return await Context.Usuarios.FirstOrDefaultAsync(s => s.UsuarioId == Id);
        }
    }
}
