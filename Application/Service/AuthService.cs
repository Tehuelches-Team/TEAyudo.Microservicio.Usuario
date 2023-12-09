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


namespace Application.Service
{
    public class AuthService : IAuthService
    {
        private readonly IUsuarioService _UsuarioService;
        private readonly string _SecretKey = "83kZz7QOdv9Sj3SqT1gS0sjTPqmGDqo8XVXzNDLL";
        private readonly string _issuer = "https://securetoken.google.com/chatteayudo";
        private readonly string _audience = "chatteayudo";
        public AuthService(IUsuarioService UsuarioService)
        {
            _UsuarioService = UsuarioService;
        }

        public string GenerateToken(string email, string password)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = _SecretKey;
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Email, email),
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                Issuer = _issuer,
                Audience = _audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }


    }


}

