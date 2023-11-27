using Application.Interface;
using Application.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace TEAyudo_Usuarios.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _AuthService;
        public AuthController(IAuthService AuthService)
        {
            _AuthService = AuthService;
        }
        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            var token = _AuthService.GenerateToken(email, password);

            // Devolver el token en el encabezado de la respuesta y el payload en el cuerpo de la respuesta
            if(email == "matias@gmasdas.com" && password == "123123213")
            {
                Response.Headers.Add("Authorization", $"Bearer {token}");
                return Ok(new { token });

            }
            else {                 
                return Unauthorized();
            }
            

        }

    }
}
