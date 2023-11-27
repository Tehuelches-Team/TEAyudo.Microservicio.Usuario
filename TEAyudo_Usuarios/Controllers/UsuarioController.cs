
using Application.Interface;
using Application.Model.DTO;
using Application.Model.Response;
using Application.Service;
using Application.Validation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tools;

namespace TEAyudo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService UsuarioService;
        private readonly IAuthService _AuthService;

        public UsuarioController(IUsuarioService UsuarioService, IAuthService AuthService)
        {
            this.UsuarioService = UsuarioService;
            this._AuthService = AuthService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsuarios()
        {
            List<UsuarioResponse> ListaResponse = await UsuarioService.GetAllUsuarios();
            if (ListaResponse.Count == 0)
            {
                var ObjetoAnonimo = new
                {
                    Mensaje = "No hay usuarios registrados actualmente."
                };
                return NotFound(ObjetoAnonimo);
            }
            return Ok(ListaResponse);
        }

        [HttpGet("{correo}/{contrasena}")]
        public async Task<IActionResult> GetAllUsuarios(string correo, string contrasena)
        {
            LogginResponse? result = await UsuarioService.Loggin(correo,contrasena);
            if (result == null)
            {
                var ObjetoAnonimo = new
                {
                    Mensaje = "Usuario no encontrado en la base de datos."
                };
                return NotFound(ObjetoAnonimo);
            }
            var token = _AuthService.GenerateToken(correo, contrasena);
            return new JsonResult(new { token, result }) { StatusCode = 201 };
            //return Ok(new { token} , result);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetUsuarioById(int Id)
        {
            UsuarioResponse? UsuarioResponse = await UsuarioService.GetUsuarioById(Id);
            if (UsuarioResponse == null)
            {
                var ObjetoAnonimo = new
                {
                    Mensaje = "No hay usuarios registrados asociados con el id " + Id
                };
                return NotFound(ObjetoAnonimo);
            }
            return Ok(UsuarioResponse);
        }

        [HttpPost]
        public async Task<IActionResult> postusuario(UsuarioDTO UsuarioDTO)
        {
            try //Consultar respecto al estado del usuario
            {
                ValidationFecha.Validar(UsuarioDTO.FechaNacimiento);
            }
            catch (Exception ex)
            {
                var objetoanonimo = new
                {
                    mensaje = ex.Message
                };
                return BadRequest(objetoanonimo);
            }

            UsuarioResponse? Usuarioresponse = await UsuarioService.PostUsuario(UsuarioDTO, Token.GenerarToken());
            if (Usuarioresponse == null)
            {
                var objetoanonimo = new
                {
                    mensaje = "no se ha podido crear el usuario por x motivo."
                };
                return new JsonResult(objetoanonimo) { StatusCode = 209 };
            }
            return new JsonResult(Usuarioresponse) { StatusCode = 201 };
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> PutUsuario(int Id, UsuarioDTO UsuarioDTO)
        {
            UsuarioResponse? UsuarioResponse = await UsuarioService.GetUsuarioById(Id);
            if (UsuarioResponse == null)
            {
                var ObjetoAnonimo = new
                {
                    Mensaje = "No existe un usuario asociado con el Id " + Id
                };
                return NotFound(ObjetoAnonimo);
            }

            try //Consultar respecto al estado del usuario
            {
                ValidationFecha.Validar(UsuarioDTO.FechaNacimiento);
            }
            catch (Exception ex)
            {
                var objetoanonimo = new
                {
                    mensaje = ex.Message
                };
                return BadRequest(objetoanonimo);
            }
            if (UsuarioResponse.CorreoElectronico != UsuarioDTO.CorreoElectronico)
            {
                if (await UsuarioService.ComprobarCorreo(UsuarioDTO.CorreoElectronico))
                {
                    var objetoanonimo = new
                    {
                        mensaje = "no se ha podido crear el usuario debido a que el correo electronico ya se encuentra registrado."
                    };
                    return Conflict(objetoanonimo);
                }
            }

            UsuarioResponse = await UsuarioService.PutUsuario(Id, UsuarioDTO);

            return new JsonResult(UsuarioResponse) { StatusCode = 201 };
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteUsuario(int Id)
        {
            UsuarioResponse? UsuarioResponse = await UsuarioService.GetUsuarioById(Id);
            if (UsuarioResponse == null)
            {
                var objetoanonimo = new
                {
                    mensaje = "No existe un usuario con el Id " + Id
                };
                return NotFound(objetoanonimo);
            }
            await UsuarioService.DeleteUsuario(Id);
            return Ok(UsuarioResponse);
        }
    }
}
