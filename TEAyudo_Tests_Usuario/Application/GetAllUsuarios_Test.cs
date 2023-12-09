using Application.Interface;
using Application.Service;
using Domain.Entities;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TEAyudo_Tests_Usuario.Application
{
    [TestFixture]
    public class GetAllUsuarios_Test
    {
        [Test]
        public async Task GetAllUsuarios_ShouldReturnMappedResponse()
        {
            // Arrange
            var mockUsuarioCommand = new Mock<IUsuarioCommand>();
            var mockUsuarioQuery = new Mock<IUsuarioQuery>();

            // Configuración para un escenario exitoso
            var UsuariosExitosos = new List<Usuario> { /* Agrega usuarios de prueba exitosos */ };
            mockUsuarioQuery.Setup(q => q.GetAllUsuarios()).ReturnsAsync(UsuariosExitosos);

            var usuarioService = new UsuarioService(mockUsuarioCommand.Object, mockUsuarioQuery.Object);

            // Act
            var resultExitoso = await usuarioService.GetAllUsuarios();

            // Assert
            Assert.That(resultExitoso, Is.Not.Null);

            // Configuración para simular un fallo
            mockUsuarioQuery.Setup(q => q.GetAllUsuarios()).ThrowsAsync(new Exception("Error simulado"));

            // Act & Assert
            Assert.ThrowsAsync<Exception>(async () => await usuarioService.GetAllUsuarios());

            // Configuración para simular un retorno de lista vacía
            mockUsuarioQuery.Setup(q => q.GetAllUsuarios()).ReturnsAsync(new List<Usuario>());

            // Act
            var resultListaVacia = await usuarioService.GetAllUsuarios();

            // Assert
            Assert.That(resultListaVacia, Is.Not.Null);
            Assert.That(resultListaVacia, Is.Empty);

        }
    }
}
