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

namespace Application.Service
{
    public class AuthService : IAuthService
    {
        private readonly IUsuarioService _UsuarioService;
        private readonly string _SecretKey = "qwertyuiopasdfghjklzxcvbnm123456";
        private readonly string _issuer = "https://securetoken.google.com/chatteayudo";
        private readonly string _audience = "chatteayudo";
        public AuthService(IUsuarioService UsuarioService)
        {
            _UsuarioService = UsuarioService;
        }

        public string GenerateToken(string nombre, string apellido, string foto, string correo)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = _SecretKey;
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("nombre", nombre),
                    new Claim("apellido", apellido),
                    new Claim("foto", foto),
                    new Claim("email", correo),
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                Issuer = _issuer,
                Audience = _audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)), SecurityAlgorithms.HmacSha256Signature)
            }; 
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public Task<string> VerificarToken(string token)
        {
            // Decodificar y validar el token recibido
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = _issuer,
                ValidateAudience = true,
                ValidAudience = _audience,
                ValidateLifetime = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_SecretKey)),
                ValidateIssuerSigningKey = true,
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken validatedToken;

            var principal = tokenHandler.ValidateToken(token, validationParameters, out validatedToken);

            // El token es válido, puedes acceder a la información del usuario desde 'principal'

            var NombreUsuario = principal.Claims.First(claim => claim.Type == "name").Value;
            string[] nombres = NombreUsuario.Split(" ");
            string nombre = nombres[0];
            string apellido = nombres[1];
            var correo = principal.Claims.First(claim => claim.Type == "email").Value;
            var foto = principal.Claims.First(claim => claim.Type == "picture").Value;

            var NuevoToken = GenerateToken(nombre, apellido, foto, correo);
            return Task.FromResult(NuevoToken);
        }
    }

        //public async Task<string> VerificarToken(string token)
        //{

        //    //Decodificar token
        //    JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
        //    var jsonToken = tokenHandler.ReadJwtToken(token);
        //    // Obtener el valor de la cabecera "kid" del token original
        //    var kid = jsonToken?.Header?.GetValueOrDefault("kid")?.ToString();
        //    //var hmac = new HMACSHA512(Encoding.UTF8.GetBytes(_SecretKey));
        //    var hmac = Encoding.ASCII.GetBytes(_SecretKey);
        //    /*var validar = tokenHandler.ValidateToken(token, new TokenValidationParameters
        //    {
        //        IssuerSigningKey = new SymmetricSecurityKey(hmac),
        //        ValidateIssuer = false,
        //        ValidateAudience = false
        //    }, out SecurityToken validatedToken);*/

        //    var NombreUsuario = jsonToken.Claims.First(claim => claim.Type == "name").Value;
        //    string[] nombres = NombreUsuario.Split(" ");
        //    string nombre = nombres[0];
        //    string apellido = nombres[1];
        //    var correo = jsonToken.Claims.First(claim => claim.Type == "email").Value;
        //    var foto = jsonToken.Claims.First(claim => claim.Type == "picture").Value;

        //    var NuevoToken = GenerateToken(nombre, apellido, foto, correo);
        //    return NuevoToken;

        //}
    }
}
