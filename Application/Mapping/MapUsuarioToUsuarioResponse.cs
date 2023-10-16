using Application.Model.Response;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mapping
{
    public class MapUsuarioToUsuarioResponse
    {
        public UsuarioResponse Map (Usuario Usuario) 
        {
            UsuarioResponse UsuarioResponse = new UsuarioResponse
            {
                UsuarioId = Usuario.UsuarioId,
                Nombre = Usuario.Nombre,
                Apellido = Usuario.Apellido,
                CorreoElectronico = Usuario.CorreoElectronico,
                Contrasena = Usuario.Contrasena,
                FotoPerfil = Usuario.FotoPerfil,
                Domicilio = Usuario.Domicilio,
                FechaNacimiento = Usuario.FechaNacimiento.ToString("dd/MM/yyyy"),
                EstadoUsuarioId = Usuario.EstadoUsuarioId,
                Token = Usuario.Token
            };
            return UsuarioResponse;
        }
    }
}
