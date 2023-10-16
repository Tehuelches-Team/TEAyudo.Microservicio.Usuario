using Application.Interface;
using Application.Model.DTO;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Commands
{
    public class UsuarioCommand : IUsuarioCommand
    {
        private readonly TEAyudoContext Context;
        public UsuarioCommand(TEAyudoContext Context)
        {
            this.Context = Context;
        }

        public async Task<Usuario?> PostUsuario(Usuario Usuario)
        {
            Context.Add(Usuario);
            await Context.SaveChangesAsync();
            return await Context.Usuarios.FirstOrDefaultAsync(s => s.UsuarioId == Usuario.UsuarioId);
        }

        public async Task<Usuario> PutUsuari aso(int Id,UsuarioDTO UsuarioRecibido)
        {
            Usuario? Usuario = await Context.Usuarios.FirstOrDefaultAsync(s => s.UsuarioId == Id);

            Usuario.Nombre = UsuarioRecibido.Nombre;
            Usuario.Apellido = UsuarioRecibido.Apellido;
            Usuario.CorreoElectronico = UsuarioRecibido.CorreoElectronico;
            Usuario.Contrasena = UsuarioRecibido.Contrasena;
            Usuario.Domicilio = UsuarioRecibido.Domicilio;
            Usuario.FotoPerfil = UsuarioRecibido.FotoPerfil;
            Usuario.FechaNacimiento = DateTime.Parse(UsuarioRecibido.FechaNacimiento);
            Context.SaveChanges();
            return Usuario; //Verificar que funcione
        }
    }
}
