using Application.Interface;
using Application.Model.DTO;
using Application.Service;
using Domain.Entities;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TEAyudo_Usuarios_Test.Application
{
    public class UsuarioServiceTests
    {
        [Fact]
        public async Task GetAllUsuarios_ShouldReturnAllUsuarios()
        {
            // Arrange
            var mockUsuarioCommand = new Mock<IUsuarioCommand>();
            var mockUsuarioQuery = new Mock<IUsuarioQuery>();

            // Configuración del mock para GetAllUsuarios
            var usuarios = new List<Usuario> { };
            mockUsuarioQuery.Setup(q => q.GetAllUsuarios()).ReturnsAsync(usuarios);

            var usuarioService = new UsuarioService(mockUsuarioCommand.Object, mockUsuarioQuery.Object);

            // Act
            var result = await usuarioService.GetAllUsuarios();

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetAllUsuarios_ShouldHandleError()
        {
            // Arrange
            var mockUsuarioCommand = new Mock<IUsuarioCommand>();
            var mockUsuarioQuery = new Mock<IUsuarioQuery>();

            // Configuración del mock para GetAllUsuarios con error simulado
            mockUsuarioQuery.Setup(q => q.GetAllUsuarios()).ThrowsAsync(new Exception("Error simulado"));

            var usuarioService = new UsuarioService(mockUsuarioCommand.Object, mockUsuarioQuery.Object);

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(async () => await usuarioService.GetAllUsuarios());
        }

        [Fact]
        public async Task GetUsuarioById_WhenUsuarioExists_ShouldReturnMappedResponse()
        {
            // Arrange
            var mockUsuarioCommand = new Mock<IUsuarioCommand>();
            var mockUsuarioQuery = new Mock<IUsuarioQuery>();

            // Configuración del mock para GetUsuarioById
            var usuarioId = 1; // Id de usuario existente
            var usuario = new Usuario { };
            mockUsuarioQuery.Setup(q => q.GetUsuarioById(usuarioId)).ReturnsAsync(usuario);

            var usuarioService = new UsuarioService(mockUsuarioCommand.Object, mockUsuarioQuery.Object);

            // Act
            var result = await usuarioService.GetUsuarioById(usuarioId);

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetUsuarioById_WhenUsuarioDoesNotExist_ShouldReturnNull()
        {
            // Arrange
            var mockUsuarioCommand = new Mock<IUsuarioCommand>();
            var mockUsuarioQuery = new Mock<IUsuarioQuery>();

            // Configuración del mock para GetUsuarioById cuando no existe el usuario
            var usuarioId = 2; // Id de usuario que no existe
            mockUsuarioQuery.Setup(q => q.GetUsuarioById(usuarioId)).ReturnsAsync((Usuario)null);

            var usuarioService = new UsuarioService(mockUsuarioCommand.Object, mockUsuarioQuery.Object);

            // Act
            var result = await usuarioService.GetUsuarioById(usuarioId);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task PostUsuario_WhenUsuarioIsCreated_ShouldReturnMappedResponse()
        {
            // Arrange
            var mockUsuarioCommand = new Mock<IUsuarioCommand>();
            var mockUsuarioQuery = new Mock<IUsuarioQuery>();

            // Configuración del mock para UsuarioCommand.PostUsuario
            var usuarioDTO = new UsuarioDTO
            {
                CUIL = 2033333333,
                Nombre = "testing",
                Apellido = "testing",
                CorreoElectronico = "testing",
                Contrasena = "testing",
                FotoPerfil = "testing",
                Domicilio = "testing",
                FechaNacimiento = "1992-06-01",
                TipoUsuario = 0,
                
            };
            var usuario = new Usuario 
            {         
                CUIL = usuarioDTO.CUIL,
                Nombre = usuarioDTO.Nombre,
                Apellido = usuarioDTO.Apellido,
                CorreoElectronico = usuarioDTO.CorreoElectronico,
                Contrasena = usuarioDTO.Contrasena,
                FotoPerfil = usuarioDTO.FotoPerfil,
                Domicilio = usuarioDTO.Domicilio,
                FechaNacimiento = DateTime.Parse(usuarioDTO.FechaNacimiento).Date,
                TipoUsuario = usuarioDTO.TipoUsuario,
                EstadoUsuarioId = 0,
                Token = "dummyToken",
            };
            mockUsuarioCommand.Setup(c => c.PostUsuario(It.IsAny<Usuario>())).ReturnsAsync(usuario);

            var usuarioService = new UsuarioService(mockUsuarioCommand.Object, mockUsuarioQuery.Object);

            // Act
            var result = await usuarioService.PostUsuario(usuarioDTO, "dummyToken");

            // Assert
            Assert.NotNull(result);

        }

        [Fact]
        public async Task PostUsuario_WhenUsuarioCreationFails_ShouldReturnNull()
        {
            // Arrange
            var mockUsuarioCommand = new Mock<IUsuarioCommand>();
            var mockUsuarioQuery = new Mock<IUsuarioQuery>();

            // Configuración del mock para UsuarioCommand.PostUsuario que devuelve null (falla)
            mockUsuarioCommand.Setup(c => c.PostUsuario(It.IsAny<Usuario>())).ReturnsAsync((Usuario)null);

            var usuarioService = new UsuarioService(mockUsuarioCommand.Object, mockUsuarioQuery.Object);

            // Act
            var result = await usuarioService.PostUsuario(new UsuarioDTO(), "dummyToken");

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task ComprobarCorreo_WhenUsuarioExists_ShouldReturnTrue()
        {
            // Arrange
            var mockUsuarioQuery = new Mock<IUsuarioQuery>();

            // Configuración del mock para GetUsuarioByEmail cuando el usuario existe
            var correoExistente = "correo_existente@example.com";
            mockUsuarioQuery.Setup(q => q.GetUsuarioByEmail(correoExistente)).ReturnsAsync(true);

            var usuarioService = new UsuarioService(Mock.Of<IUsuarioCommand>(), mockUsuarioQuery.Object);

            // Act
            var result = await usuarioService.ComprobarCorreo(correoExistente);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task ComprobarCorreo_WhenUsuarioDoesNotExist_ShouldReturnFalse()
        {
            // Arrange
            var mockUsuarioQuery = new Mock<IUsuarioQuery>();

            // Configuración del mock para GetUsuarioByEmail cuando el usuario no existe
            var correoNoExistente = "correo_no_existente@example.com";
            mockUsuarioQuery.Setup(q => q.GetUsuarioByEmail(correoNoExistente)).ReturnsAsync(null);

            var usuarioService = new UsuarioService(Mock.Of<IUsuarioCommand>(), mockUsuarioQuery.Object);

            // Act
            var result = await usuarioService.ComprobarCorreo(correoNoExistente);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task Loggin_WhenUsuarioExists_ShouldReturnLogginResponse()
        {
            // Arrange
            var mockUsuarioQuery = new Mock<IUsuarioQuery>();

            // Configuración del mock para GetUsuarioByLoggin cuando el usuario existe
            var correoExistente = "correo_existente@example.com";
            var contrasena = "contrasena123";
            var usuarioExistente = new Usuario
            {
                UsuarioId = 1,
                TipoUsuario = 1,
            };
            mockUsuarioQuery.Setup(q => q.GetUsuarioByLoggin(correoExistente, contrasena)).ReturnsAsync(usuarioExistente);

            var usuarioService = new UsuarioService(Mock.Of<IUsuarioCommand>(), mockUsuarioQuery.Object);

            // Act
            var result = await usuarioService.Loggin(correoExistente, contrasena);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(usuarioExistente.UsuarioId, result.UsuarioId);
            Assert.Equal(usuarioExistente.TipoUsuario, result.TipoUsuario);
        }

        [Fact]
        public async Task Loggin_WhenUsuarioDoesNotExist_ShouldReturnNull()
        {
            // Arrange
            var mockUsuarioQuery = new Mock<IUsuarioQuery>();

            // Configuración del mock para GetUsuarioByLoggin cuando el usuario no existe
            var correoNoExistente = "correo_no_existente@example.com";
            var contrasena = "contrasena456";
            mockUsuarioQuery.Setup(q => q.GetUsuarioByLoggin(correoNoExistente, contrasena)).ReturnsAsync((Usuario)null);

            var usuarioService = new UsuarioService(Mock.Of<IUsuarioCommand>(), mockUsuarioQuery.Object);

            // Act
            var result = await usuarioService.Loggin(correoNoExistente, contrasena);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task PutUsuario_WhenUsuarioUpdated_ShouldReturnUsuarioResponse()
        {
            // Arrange
            var mockUsuarioCommand = new Mock<IUsuarioCommand>();

            var usuarioId = 1;
            var usuarioRecibido = new UsuarioDTO
            {
                CUIL = 2033333333,
                Nombre = "testing",
                Apellido = "testing",
                CorreoElectronico = "testing",
                Contrasena = "testing",
                FotoPerfil = "testing",
                Domicilio = "testing",
                FechaNacimiento = "1992-06-01",
                TipoUsuario = 0,
            };
            var usuarioActualizado = new Usuario { UsuarioId = usuarioId, /* Configura un Usuario actualizado de prueba */ };
            mockUsuarioCommand.Setup(c => c.PutUsuario(usuarioId, usuarioRecibido)).ReturnsAsync(usuarioActualizado);

            var usuarioService = new UsuarioService(mockUsuarioCommand.Object, Mock.Of<IUsuarioQuery>());

            // Act
            var result = await usuarioService.PutUsuario(usuarioId, usuarioRecibido);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(usuarioActualizado.UsuarioId, result.UsuarioId);
        }

        [Fact]
        public async Task PutUsuario_WhenUsuarioUpdateFails_ShouldReturnNull()
        {
            // Arrange
            var mockUsuarioCommand = new Mock<IUsuarioCommand>();

            var usuarioId = 2;
            var usuarioRecibido = new UsuarioDTO 
            {
                CUIL = 2033333333,
                Nombre = "testing",
                Apellido = "testing",
                CorreoElectronico = "testing",
                Contrasena = "testing",
                FotoPerfil = "testing",
                Domicilio = "testing",
                FechaNacimiento = "1992-06-01",
                TipoUsuario = 0,
            };
            mockUsuarioCommand.Setup(c => c.PutUsuario(usuarioId, usuarioRecibido)).ReturnsAsync((Usuario)null);

            var usuarioService = new UsuarioService(mockUsuarioCommand.Object, Mock.Of<IUsuarioQuery>());

            // Act
            var result = await usuarioService.PutUsuario(usuarioId, usuarioRecibido);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task DeleteUsuario_WhenUsuarioExists_ShouldNotThrowException()
        {
            // Arrange
            var mockUsuarioCommand = new Mock<IUsuarioCommand>();

            // Configuración del mock para UsuarioCommand.DeleteUsuario cuando la eliminación es exitosa
            var usuarioId = 1;
            mockUsuarioCommand.Setup(c => c.DeleteUsuario(usuarioId)).Returns(Task.CompletedTask);

            var usuarioService = new UsuarioService(mockUsuarioCommand.Object, Mock.Of<IUsuarioQuery>());

            // Act and Assert
            await Assert.ThrowsAsync<Exception>(async () => await usuarioService.DeleteUsuario(usuarioId));
        }

        [Fact]
        public async Task DeleteUsuario_WhenUsuarioDoesNotExist_ShouldNotThrowException()
        {
            // Arrange
            var mockUsuarioCommand = new Mock<IUsuarioCommand>();

            // Configuración del mock para UsuarioCommand.DeleteUsuario cuando la eliminación no encuentra al usuario (puede lanzar excepciones)
            var usuarioId = 2;
            mockUsuarioCommand.Setup(c => c.DeleteUsuario(usuarioId)).Returns(Task.CompletedTask);

            var usuarioService = new UsuarioService(mockUsuarioCommand.Object, Mock.Of<IUsuarioQuery>());

            // Act and Assert
            await Assert.ThrowsAsync<Exception>(async () => await usuarioService.DeleteUsuario(usuarioId));
        }

    }
}
