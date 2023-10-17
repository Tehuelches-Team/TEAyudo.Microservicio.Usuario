using Application.Model.DTO;
using Domain.Entities;

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
                //Token, hablar con Marce
                EstadoUsuarioId = 0,
            };
        }
    }
}
