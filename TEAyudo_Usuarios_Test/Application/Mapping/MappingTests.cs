using Application.Mapping;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TEAyudo_Usuarios_Test.Application.Mapping
{
    public class MappingTests
    {
        [Fact]
        public void Map_WhenGivenListOfUsuarios_ShouldReturnListOfUsuarioResponses()
        {
            // Arrange
            var mapUsuariosToUsuariosResponse = new MapUsuariosToUsuariosResponse();

            var listaUsuarios = new List<Usuario>
            {
                new Usuario { UsuarioId = 1, Nombre = "Usuario1" },
                new Usuario { UsuarioId = 2, Nombre = "Usuario2" }
                // Agrega más usuarios según sea necesario para tus pruebas
            };

            // Act
            var listaResponse = mapUsuariosToUsuariosResponse.Map(listaUsuarios);

            // Assert
            Assert.NotNull(listaResponse);
            Assert.Equal(listaUsuarios.Count, listaResponse.Count);

            for (int i = 0; i < listaUsuarios.Count; i++)
            {
                Assert.Equal(listaUsuarios[i].UsuarioId, listaResponse[i].UsuarioId);
                Assert.Equal(listaUsuarios[i].Nombre, listaResponse[i].Nombre);
                // Asegúrate de agregar más aserciones según las propiedades que tengas en Usuario y UsuarioResponse
            }
        }
    }
}
