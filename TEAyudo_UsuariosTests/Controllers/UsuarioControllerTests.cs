using Microsoft.VisualStudio.TestTools.UnitTesting;
using TEAyudo.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Application.Interface;
using Moq;
using Domain.Entities;
using Application.Service;

namespace TEAyudo.Controllers.Tests
{
    [TestClass()]
    public class UsuarioControllerTests
    {
        [Fact]
        public void GetAllUsuariosTest()
        {
            // Arrange
            var mockUsuarioCommand = new Mock<IUsuarioCommand>();
            var mockUsuarioQuery = new Mock<IUsuarioQuery>();

            var UsuariosExitosos = new List<Usuario> { /* Agrega usuarios de prueba exitosos */ };
            mockUsuarioQuery.Setup(q => q.GetAllUsuarios()).ReturnsAsync(UsuariosExitosos);

            var usuarioService = new UsuarioService(mockUsuarioCommand.Object, mockUsuarioQuery.Object);
            // Act
            var resultExitoso =  usuarioService.GetAllUsuarios();
            // Assert
            Assert.IsNotNull(resultExitoso);


        }

        [Fact]
        public void GetAllUsuariosTest1()
        {
            Assert.Fail();
        }

        [Fact]
        public void GetUsuarioByIdTest()
        {
            Assert.Fail();
        }

        [Fact]
        public void postusuarioTest()
        {
            Assert.Fail();
        }

        [Fact]
        public void PutUsuarioTest()
        {
            Assert.Fail();
        }

        [Fact]
        public void DeleteUsuarioTest()
        {
            Assert.Fail();
        }
    }
}