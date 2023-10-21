using Application.Model.Response;
using Domain.Entities;

namespace Application.Mapping
{
    public class MapUsuarioToUsuarioResponse
    {
        public UsuarioResponse Map(Usuario Usuario)
        {
            UsuarioResponse UsuarioResponse = new UsuarioResponse
            {
                UsuarioId = Usuario.UsuarioId,
                CUIL = Usuario.CUIL,
                Nombre = Usuario.Nombre,
                Apellido = Usuario.Apellido,
                CorreoElectronico = Usuario.CorreoElectronico,
                Contrasena = Usuario.Contrasena,
                FotoPerfil = Usuario.FotoPerfil,
                Domicilio = Usuario.Domicilio,
                FechaNacimiento = Usuario.FechaNacimiento.ToString("dd/MM/yyyy"),
                EstadoUsuarioId = Usuario.EstadoUsuarioId,
            };
            return UsuarioResponse;
        }
    }
}
