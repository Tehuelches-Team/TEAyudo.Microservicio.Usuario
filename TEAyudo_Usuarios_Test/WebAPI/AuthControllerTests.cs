using Application.Interface;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interface;
using Application.Model.Response;
using Microsoft.IdentityModel.Tokens;
using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using Microsoft.Web.Services3.Security.Tokens;
using System.Security.Cryptography.X509Certificates;
using TEAyudo_Usuarios.Controllers;

namespace TEAyudo_Usuarios_Test.WebAPI
{
    public class AuthControllerTests
    {
        [Fact]
        public async Task Login_WithValidCredentials_ShouldReturnOkWithToken()
        {
            // Arrange
            var mockAuthService = new Mock<IAuthService>();
            mockAuthService.Setup(x => x.GenerateToken(It.IsAny<string>(), It.IsAny<string>())).Returns("dummyToken");
            var authController = new AuthController(mockAuthService.Object);

            // Act
            var result = await authController.Login("matias@gmasdas.com", "123123213") as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            //Assert.Equal("Bearer dummyToken", result.Headers["Authorization"]);
            Assert.Equal("dummyToken", result.Value);
        }

        [Fact]
        public async Task Login_WithInvalidCredentials_ShouldReturnUnauthorized()
        {
            // Arrange
            var mockAuthService = new Mock<IAuthService>();
            mockAuthService.Setup(x => x.GenerateToken(It.IsAny<string>(), It.IsAny<string>())).Returns("dummyToken");
            var authController = new AuthController(mockAuthService.Object);

            // Act
            var result = await authController.Login("invalidEmail", "invalidPassword") as UnauthorizedResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(401, result.StatusCode);
        }
    }
}
