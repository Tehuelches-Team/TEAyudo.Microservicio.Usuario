using Application.Model.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TEAyudo_Usuarios_Test.Application.Model
{
    public class ModelTests
    {
        [Fact]
        public void UsuarioResponse_Properties_ShouldHaveCorrectGettersAndSetters()
        {
            // Arrange
            var usuarioResponse = new UsuarioResponse();

            // Act and Assert
            Assert.Equal(0, usuarioResponse.UsuarioId);
            Assert.Equal(0, usuarioResponse.CUIL);
            Assert.Null(usuarioResponse.Nombre);
            Assert.Null(usuarioResponse.Apellido);
            Assert.Null(usuarioResponse.CorreoElectronico);
            Assert.Null(usuarioResponse.Contrasena);
            Assert.Null(usuarioResponse.FotoPerfil);
            Assert.Null(usuarioResponse.Domicilio);
            Assert.Null(usuarioResponse.FechaNacimiento);
            Assert.Null(usuarioResponse.EstadoUsuarioId);

            usuarioResponse.UsuarioId = 1;
            usuarioResponse.CUIL = 123456789;
            usuarioResponse.Nombre = "John";
            usuarioResponse.Apellido = "Doe";
            usuarioResponse.CorreoElectronico = "john.doe@example.com";
            usuarioResponse.Contrasena = "password123";
            usuarioResponse.FotoPerfil = "profile.jpg";
            usuarioResponse.Domicilio = "123 Main St";
            usuarioResponse.FechaNacimiento = "1990-01-01";
            usuarioResponse.EstadoUsuarioId = 2;

            // Assert 
            Assert.Equal(1, usuarioResponse.UsuarioId);
            Assert.Equal(123456789, usuarioResponse.CUIL);
            Assert.Equal("John", usuarioResponse.Nombre);
            Assert.Equal("Doe", usuarioResponse.Apellido);
            Assert.Equal("john.doe@example.com", usuarioResponse.CorreoElectronico);
            Assert.Equal("password123", usuarioResponse.Contrasena);
            Assert.Equal("profile.jpg", usuarioResponse.FotoPerfil);
            Assert.Equal("123 Main St", usuarioResponse.Domicilio);
            Assert.Equal("1990-01-01", usuarioResponse.FechaNacimiento);
            Assert.Equal(2, usuarioResponse.EstadoUsuarioId);
        }
    }
}
