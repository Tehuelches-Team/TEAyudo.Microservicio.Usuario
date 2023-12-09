using Application.Interface;
using Application.Service;
using Microsoft.IdentityModel.Tokens;
using Moq;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TEAyudo_Usuarios_Test.Application
{
    public class AuthServiceTests
    {
        private readonly Mock<IUsuarioService> _mockUsuarioService;

        public AuthServiceTests()
        {
            _mockUsuarioService = new Mock<IUsuarioService>();
        }

        [Fact]
        public void GenerateToken_ShouldGenerateValidJwtToken()
        {
            // Arrange
            var authService = new AuthService(_mockUsuarioService.Object);
            var email = "test@example.com";
            var password = "password123";

            // Act
            var token = authService.GenerateToken(email, password);

            // Assert
            Assert.NotNull(token);
            Assert.True(IsValidJwtToken(token));
        }

        private bool IsValidJwtToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var validationParameters = new TokenValidationParameters
            {
                ValidIssuer = "https://securetoken.google.com/chatteayudo",
                ValidAudience = "chatteayudo",
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("83kZz7QOdv9Sj3SqT1gS0sjTPqmGDqo8XVXzNDLL")),
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };

            SecurityToken validatedToken;
            try
            {
                tokenHandler.ValidateToken(token, validationParameters, out validatedToken);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
