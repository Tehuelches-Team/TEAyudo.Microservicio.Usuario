using Application.Exceptions;
using Application.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TEAyudo_Usuarios_Test.Application.Validations
{
    public class ValidationsTests
    {
        [Fact]
        public void Validar_WhenValidDateFormat_ShouldReturnParsedDateTime()
        {
            // Arrange
            var fechaString = "2022-01-01";

            // Act
            var result = ValidationFecha.Validar(fechaString);

            // Assert
            Assert.Equal(new DateTime(2022, 01, 01), result);
        }

        [Fact]
        public void Validar_WhenInvalidDateFormat_ShouldThrowException()
        {
            // Arrange
            var invalidFechaString = "invalid-date";

            // Act and Assert
            var exception = Assert.Throws<ExceptionFecha>(() => ValidationFecha.Validar(invalidFechaString));
            Assert.Equal("La fecha no se ingresó en un formato válido.", exception.Message);
        }

        [Fact]
        public void Validar_WhenSlashInDateFormat_ShouldThrowException()
        {
            // Arrange
            var fechaStringWithSlash = "2022/01/01";

            // Act and Assert
            var exception = Assert.Throws<ExceptionFecha>(() => ValidationFecha.Validar(fechaStringWithSlash));
            Assert.Equal("La fecha se debe ingresar con \"-\" no con \"/\".", exception.Message);
        }
    }
}
