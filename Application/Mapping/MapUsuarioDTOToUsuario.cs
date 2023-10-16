using Application.Model.DTO;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mapping
{
    public class MapUsuarioDTOToUsuario
    {
        public Usuario Map(UsuarioDTO UsuarioRecibido)
        {
            return new Usuario
            {
                Nombre = UsuarioRecibido.Nombre,
                Apellido = UsuarioRecibido.Apellido,
                CorreoElectronico = UsuarioRecibido.CorreoElectronico,
                Contrasena = UsuarioRecibido.Contrasena,
                FotoPerfil = UsuarioRecibido.FotoPerfil,
                Domicilio = UsuarioRecibido.Domicilio,
                FechaNacimiento = DateTime.Parse(UsuarioRecibido.FechaNacimiento).Date,
            };
        }
    }
}
