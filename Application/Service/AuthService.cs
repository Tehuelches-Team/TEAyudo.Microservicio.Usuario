using Application.Interface;
using Application.Model.Response;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Service
{
    public class AuthService : IAuthService
    {
        private readonly IUsuarioService _UsuarioService;
        public AuthService(IUsuarioService UsuarioService)
        {
            _UsuarioService = UsuarioService;
        }
        public Task<UsuarioResponse?> VerificarToken(string token)
        {
            //Decodificar token
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("pRoG@m@doR4");
            var validar = tokenHandler.ValidateToken(token, new Microsoft.IdentityModel.Tokens.TokenValidationParameters
            {
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuerSigningKey = true,
                ValidateIssuer = false,
                ValidateAudience = false
            }, out SecurityToken validatedToken);
            

        }
    }
}
