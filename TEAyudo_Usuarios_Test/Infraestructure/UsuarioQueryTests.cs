using Domain.Entities;
using Infrastructure.Persistence;
using Infrastructure.Querys;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TEAyudo_Usuarios_Test.Infraestructure
{
    public class UsuarioQueryTests
    {
        [Fact]
        public async Task GetAllUsuarios_WithData_ShouldReturnList()
        {
            // Arrange
            var mockContext = new Mock<TEAyudoContext>();
            var mockDbSet = new Mock<DbSet<Usuario>>();

            var expectedData = new List<Usuario>
        {
            new Usuario { UsuarioId = 1, Nombre = "Usuario1" },
            new Usuario { UsuarioId = 2, Nombre = "Usuario2" }
        };

            mockDbSet.As<IQueryable<Usuario>>().Setup(m => m.Provider).Returns(expectedData.AsQueryable().Provider);
            mockDbSet.As<IQueryable<Usuario>>().Setup(m => m.Expression).Returns(expectedData.AsQueryable().Expression);
            mockDbSet.As<IQueryable<Usuario>>().Setup(m => m.ElementType).Returns(expectedData.AsQueryable().ElementType);
            mockDbSet.As<IQueryable<Usuario>>().Setup(m => m.GetEnumerator()).Returns(expectedData.AsQueryable().GetEnumerator());

            mockContext.Setup(c => c.Usuarios).Returns(mockDbSet.Object);

            var usuarioQuery = new UsuarioQuery(mockContext.Object);

            // Act
            var result = await usuarioQuery.GetAllUsuarios();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedData.Count, result.Count);
            Assert.Equal(expectedData, result, new UsuarioComparer());
        }

        [Fact]
        public async Task GetAllUsuarios_WithoutData_ShouldReturnEmptyList()
        {
            // Arrange
            var mockContext = new Mock<TEAyudoContext>();
            var mockDbSet = new Mock<DbSet<Usuario>>();

            var expectedData = new List<Usuario>();

            mockDbSet.As<IQueryable<Usuario>>().Setup(m => m.Provider).Returns(expectedData.AsQueryable().Provider);
            mockDbSet.As<IQueryable<Usuario>>().Setup(m => m.Expression).Returns(expectedData.AsQueryable().Expression);
            mockDbSet.As<IQueryable<Usuario>>().Setup(m => m.ElementType).Returns(expectedData.AsQueryable().ElementType);
            mockDbSet.As<IQueryable<Usuario>>().Setup(m => m.GetEnumerator()).Returns(expectedData.AsQueryable().GetEnumerator());

            mockContext.Setup(c => c.Usuarios).Returns(mockDbSet.Object);

            var usuarioQuery = new UsuarioQuery(mockContext.Object);

            // Act
            var result = await usuarioQuery.GetAllUsuarios();

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }

        // Puedes agregar más pruebas según sea necesario
    }

    // Clase auxiliar para comparar objetos Usuario en las pruebas
    public class UsuarioComparer : IEqualityComparer<Usuario>
    {
        public bool Equals(Usuario x, Usuario y)
        {
            return x.UsuarioId == y.UsuarioId && x.Nombre == y.Nombre;
        }

        public int GetHashCode(Usuario obj)
        {
            return obj.UsuarioId.GetHashCode();
        }
    }
}

