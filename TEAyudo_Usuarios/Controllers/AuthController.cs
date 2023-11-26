using Application.Interface;
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
        public async Task<IActionResult> Login(string token)
        {
            try 
            {
                var response = await _AuthService.VerificarToken(token);
                return Ok(new { Token = response });
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
            
        }

    }
}
