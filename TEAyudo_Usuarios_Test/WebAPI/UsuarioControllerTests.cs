using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interface;
using Application.Model.DTO;
using Application.Model.Response;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TEAyudo.Controllers;

namespace TEAyudo_Usuarios_Test.WebAPI
{
    public class UsuarioControllerTests
    {
        [Fact]
        public async Task GetAllUsuarios_WithUsuarios_ShouldReturnOkWithUsuarios()
        {
            // Arrange
            var mockUsuarioService = new Mock<IUsuarioService>();
            var mockAuthService = new Mock<IAuthService>();
            var usuarioController = new UsuarioController(mockUsuarioService.Object, mockAuthService.Object);

            var usuariosResponse = new List<UsuarioResponse>
            {
                new UsuarioResponse { UsuarioId = 1, Nombre = "Usuario1" },
                new UsuarioResponse { UsuarioId = 2, Nombre = "Usuario2" }
                // Agrega más usuarios según sea necesario para tus pruebas
            };

            mockUsuarioService.Setup(x => x.GetAllUsuarios()).ReturnsAsync(usuariosResponse);

            // Act
            var result = await usuarioController.GetAllUsuarios() as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            Assert.Equal(usuariosResponse, result.Value);
        }

        [Fact]
        public async Task GetAllUsuarios_WithoutUsuarios_ShouldReturnNotFound()
        {
            // Arrange
            var mockUsuarioService = new Mock<IUsuarioService>();
            var mockAuthService = new Mock<IAuthService>();
            var usuarioController = new UsuarioController(mockUsuarioService.Object, mockAuthService.Object);

            // Configuración del mock para que retorne una lista vacía
            mockUsuarioService.Setup(x => x.GetAllUsuarios()).ReturnsAsync(new List<UsuarioResponse>());

            // Act
            var result = await usuarioController.GetAllUsuarios() as NotFoundObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(404, result.StatusCode);
            Assert.Equal("No hay usuarios registrados actualmente.", result.Value.GetType().GetProperty("Mensaje").GetValue(result.Value, null));
        }

        [Fact]
        public async Task GetAllUsuarios_WithValidCredentials_ShouldReturn201WithTokenAndLogginResponse()
        {
            // Arrange
            var mockUsuarioService = new Mock<IUsuarioService>();
            var mockAuthService = new Mock<IAuthService>();
            var usuarioController = new UsuarioController(mockUsuarioService.Object, mockAuthService.Object);

            var logginResponse = new LogginResponse
            {
                UsuarioId = 1,
                TipoUsuario = 1,
            };

            mockUsuarioService.Setup(x => x.Loggin(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(logginResponse);
            mockAuthService.Setup(x => x.GenerateToken(It.IsAny<string>(), It.IsAny<string>())).Returns("dummyToken");

            // Act
            var result = await usuarioController.GetAllUsuarios("test@example.com", "password123") as JsonResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(201, result.StatusCode);
            Assert.Equal("dummyToken", result.Value.GetType().GetProperty("token").GetValue(result.Value, null));
            Assert.Equal(logginResponse, result.Value.GetType().GetProperty("result").GetValue(result.Value, null));
        }

        [Fact]
        public async Task GetAllUsuarios_WithInvalidCredentials_ShouldReturnNotFound()
        {
            // Arrange
            var mockUsuarioService = new Mock<IUsuarioService>();
            var mockAuthService = new Mock<IAuthService>();
            var usuarioController = new UsuarioController(mockUsuarioService.Object, mockAuthService.Object);

            // Configuración del mock para que retorne null (usuario no encontrado)
            mockUsuarioService.Setup(x => x.Loggin(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync((LogginResponse)null);

            // Act
            var result = await usuarioController.GetAllUsuarios("invalid@example.com", "invalidPassword") as NotFoundObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(404, result.StatusCode);
            Assert.Equal("Usuario no encontrado en la base de datos.", result.Value.GetType().GetProperty("Mensaje").GetValue(result.Value, null));
        }

        [Fact]
        public async Task PutUsuario_WithValidData_ShouldReturn201WithUsuarioResponse()
        {
            // Arrange
            var mockUsuarioService = new Mock<IUsuarioService>();
            var usuarioController = new UsuarioController(mockUsuarioService.Object, Mock.Of<IAuthService>());

            var usuarioResponse = new UsuarioResponse
            {
                UsuarioId = 1,
                Nombre = "Usuario1"
            };

            var usuarioDTO = new UsuarioDTO
            {
                Nombre = "UsuarioModificado"
            };

            mockUsuarioService.Setup(x => x.GetUsuarioById(It.IsAny<int>())).ReturnsAsync(usuarioResponse);
            mockUsuarioService.Setup(x => x.PutUsuario(It.IsAny<int>(), It.IsAny<UsuarioDTO>())).ReturnsAsync(usuarioResponse);

            // Act
            var result = await usuarioController.PutUsuario(1, usuarioDTO) as JsonResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(201, result.StatusCode);
            Assert.Equal(usuarioResponse, result.Value);
        }

        [Fact]
        public async Task PutUsuario_WithInvalidId_ShouldReturnNotFound()
        {
            // Arrange
            var mockUsuarioService = new Mock<IUsuarioService>();
            var usuarioController = new UsuarioController(mockUsuarioService.Object, Mock.Of<IAuthService>());

            // Configuración del mock para que retorne null (usuario no encontrado)
            mockUsuarioService.Setup(x => x.GetUsuarioById(It.IsAny<int>())).ReturnsAsync((UsuarioResponse)null);

            // Act
            var result = await usuarioController.PutUsuario(1, new UsuarioDTO()) as NotFoundObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(404, result.StatusCode);
            Assert.Equal("No existe un usuario asociado con el Id 1", result.Value.GetType().GetProperty("Mensaje").GetValue(result.Value, null));
        }

        [Fact]
        public async Task PutUsuario_WithInvalidDate_ShouldReturnBadRequest()
        {
            // Arrange
            var mockUsuarioService = new Mock<IUsuarioService>();
            var usuarioController = new UsuarioController(mockUsuarioService.Object, Mock.Of<IAuthService>());

            var usuarioResponse = new UsuarioResponse
            {
                UsuarioId = 1,
                Nombre = "Usuario1"
            };

            var usuarioDTO = new UsuarioDTO
            {
                FechaNacimiento = "invalidDate"
            };

            mockUsuarioService.Setup(x => x.GetUsuarioById(It.IsAny<int>())).ReturnsAsync(usuarioResponse);

            // Act
            var result = await usuarioController.PutUsuario(1, usuarioDTO) as BadRequestObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(400, result.StatusCode);
            Assert.Equal("La fecha no se ingresó en un formato válido.", result.Value.GetType().GetProperty("mensaje").GetValue(result.Value, null));
        }

        [Fact]
        public async Task PutUsuario_WithExistingCorreo_ShouldReturnConflict()
        {
            // Arrange
            var mockUsuarioService = new Mock<IUsuarioService>();
            var usuarioController = new UsuarioController(mockUsuarioService.Object, Mock.Of<IAuthService>());

            var usuarioResponse = new UsuarioResponse
            {
                UsuarioId = 1,
                Nombre = "Usuario1",
                CorreoElectronico = "existing@example.com"
            };

            var usuarioDTO = new UsuarioDTO
            {
                CorreoElectronico = "existing@example.com"
            };

            mockUsuarioService.Setup(x => x.GetUsuarioById(It.IsAny<int>())).ReturnsAsync(usuarioResponse);
            mockUsuarioService.Setup(x => x.ComprobarCorreo(It.IsAny<string>())).ReturnsAsync(true);

            // Act
            var result = await usuarioController.PutUsuario(1, usuarioDTO) as ConflictObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(409, result.StatusCode);
            Assert.Equal("no se ha podido crear el usuario debido a que el correo electronico ya se encuentra registrado.", result.Value.GetType().GetProperty("mensaje").GetValue(result.Value, null));
        }

        [Fact]
        public async Task PostUsuario_WithValidData_ShouldReturn201WithUsuarioResponse()
        {
            // Arrange
            var mockUsuarioService = new Mock<IUsuarioService>();
            var usuarioController = new UsuarioController(mockUsuarioService.Object, Mock.Of<IAuthService>());

            var usuarioDTO = new UsuarioDTO
            {
                FechaNacimiento = "2022-01-01"
            };

            mockUsuarioService.Setup(x => x.PostUsuario(It.IsAny<UsuarioDTO>(), It.IsAny<string>())).ReturnsAsync(new UsuarioResponse { UsuarioId = 1 });

            // Act
            var result = await usuarioController.postusuario(usuarioDTO) as JsonResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(201, result.StatusCode);
            Assert.Equal(new UsuarioResponse { UsuarioId = 1 }, result.Value);
        }

        [Fact]
        public async Task PostUsuario_WithInvalidDate_ShouldReturnBadRequest()
        {
            // Arrange
            var mockUsuarioService = new Mock<IUsuarioService>();
            var usuarioController = new UsuarioController(mockUsuarioService.Object, Mock.Of<IAuthService>());

            var usuarioDTO = new UsuarioDTO
            {
                FechaNacimiento = "invalidDate"
            };

            var expectedErrorMessage = "La fecha no se ingresó en un formato válido.";
            mockUsuarioService.Setup(x => x.PostUsuario(It.IsAny<UsuarioDTO>(), It.IsAny<string>())).ReturnsAsync((UsuarioResponse)null);

            // Act
            var result = await usuarioController.postusuario(usuarioDTO) as BadRequestObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(400, result.StatusCode);
            Assert.Equal(expectedErrorMessage, result.Value.GetType().GetProperty("mensaje").GetValue(result.Value, null));
        }

        [Fact]
        public async Task PostUsuario_WithPostUsuarioFailure_ShouldReturn209WithErrorMessage()
        {
            // Arrange
            var mockUsuarioService = new Mock<IUsuarioService>();
            var usuarioController = new UsuarioController(mockUsuarioService.Object, Mock.Of<IAuthService>());

            var usuarioDTO = new UsuarioDTO
            {
                FechaNacimiento = "2022-01-01"
            };

            var errorMessage = "Error al crear el usuario.";
            mockUsuarioService.Setup(x => x.PostUsuario(It.IsAny<UsuarioDTO>(), It.IsAny<string>())).ReturnsAsync((UsuarioResponse)null);

            // Act
            var result = await usuarioController.postusuario(usuarioDTO) as JsonResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(209, result.StatusCode);
            Assert.Equal(errorMessage, result.Value.GetType().GetProperty("mensaje").GetValue(result.Value, null));
        }

        [Fact]
        public async Task DeleteUsuario_WithExistingId_ShouldReturnOkWithUsuarioResponse()
        {
            // Arrange
            var mockUsuarioService = new Mock<IUsuarioService>();
            var usuarioController = new UsuarioController(mockUsuarioService.Object, Mock.Of<IAuthService>());

            var usuarioResponse = new UsuarioResponse
            {
                UsuarioId = 1,
                Nombre = "Usuario1"
            };

            mockUsuarioService.Setup(x => x.GetUsuarioById(It.IsAny<int>())).ReturnsAsync(usuarioResponse);

            // Act
            var result = await usuarioController.DeleteUsuario(1) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            Assert.Equal(usuarioResponse, result.Value);
        }

        [Fact]
        public async Task DeleteUsuario_WithNonExistingId_ShouldReturnNotFound()
        {
            // Arrange
            var mockUsuarioService = new Mock<IUsuarioService>();
            var usuarioController = new UsuarioController(mockUsuarioService.Object, Mock.Of<IAuthService>());

            // Configuración del mock para que retorne null (usuario no encontrado)
            mockUsuarioService.Setup(x => x.GetUsuarioById(It.IsAny<int>())).ReturnsAsync((UsuarioResponse)null);

            // Act
            var result = await usuarioController.DeleteUsuario(1) as NotFoundObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(404, result.StatusCode);
            Assert.Equal("No existe un usuario con el Id 1", result.Value.GetType().GetProperty("mensaje").GetValue(result.Value, null));
        }

        [Fact]
        public async Task DeleteUsuario_WithExistingId_ShouldReturnOkWithDeletedUsuario()
        {
            // Arrange
            var mockUsuarioService = new Mock<IUsuarioService>();
            var usuarioController = new UsuarioController(mockUsuarioService.Object, Mock.Of<IAuthService>());

            var usuarioResponse = new UsuarioResponse
            {
                UsuarioId = 1,
                Nombre = "Usuario1"
            };

            mockUsuarioService.Setup(x => x.GetUsuarioById(It.IsAny<int>())).ReturnsAsync(usuarioResponse);

            // Act
            var result = await usuarioController.DeleteUsuario(1) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            Assert.Equal(usuarioResponse, result.Value);

            // Verificar que se llamó a DeleteUsuario con el ID correcto
            mockUsuarioService.Verify(x => x.DeleteUsuario(1), Times.Once);
        }

        [Fact]
        public async Task GetUsuarioById_WithExistingId_ShouldReturnOkWithUsuarioResponse()
        {
            // Arrange
            var mockUsuarioService = new Mock<IUsuarioService>();
            var usuarioController = new UsuarioController(mockUsuarioService.Object, Mock.Of<IAuthService>());

            var usuarioResponse = new UsuarioResponse
            {
                UsuarioId = 1,
                Nombre = "Usuario1"
            };

            mockUsuarioService.Setup(x => x.GetUsuarioById(It.IsAny<int>())).ReturnsAsync(usuarioResponse);

            // Act
            var result = await usuarioController.GetUsuarioById(1) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            Assert.Equal(usuarioResponse, result.Value);
        }

        [Fact]
        public async Task GetUsuarioById_WithNonExistingId_ShouldReturnNotFound()
        {
            // Arrange
            var mockUsuarioService = new Mock<IUsuarioService>();
            var usuarioController = new UsuarioController(mockUsuarioService.Object, Mock.Of<IAuthService>());

            // Configuración del mock para que retorne null (usuario no encontrado)
            mockUsuarioService.Setup(x => x.GetUsuarioById(It.IsAny<int>())).ReturnsAsync((UsuarioResponse)null);

            // Act
            var result = await usuarioController.GetUsuarioById(1) as NotFoundObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(404, result.StatusCode);
            Assert.Equal("No hay usuarios registrados asociados con el id 1", result.Value.GetType().GetProperty("Mensaje").GetValue(result.Value, null));
        }

    }
}
