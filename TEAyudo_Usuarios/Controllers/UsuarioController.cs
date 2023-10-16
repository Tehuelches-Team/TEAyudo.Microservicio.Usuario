
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using TEAyudo_Usuarios;
using Tools;
using Application.Interface;
using Application.Model.Response;
using Application.Model.DTO;
using Application.Exceptions;
using Application.Validation;

namespace TEAyudo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService UsuarioService;

        public UsuarioController(IUsuarioService UsuarioService)
        {
           this.UsuarioService = UsuarioService;
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

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetUsuarioById(int Id)
        {
            UsuarioResponse? UsuarioResponse = await UsuarioService.GetUsuarioById(Id);
            if (UsuarioResponse == null)
            {
                var ObjetoAnonimo = new
                {
                    Mensaje = "No hay usuarios registrados actualmente."
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
            }catch (Exception ex)
            {
                var objetoanonimo = new
                {
                    mensaje = ex.Message
                };
                return BadRequest(objetoanonimo);
            }

            if (await UsuarioService.ComprobarCorreo(UsuarioDTO.CorreoElectronico))
            {
                var objetoanonimo = new
                {
                    mensaje = "no se ha podido crear el usuario debido a que el correo electronico ya se encuentra registrado."
                };
                return new JsonResult(objetoanonimo) { StatusCode = 209 };
            }

            UsuarioResponse? Usuarioresponse = await UsuarioService.PostUsuario(UsuarioDTO,Token.GenerarToken());
            if (Usuarioresponse == null)
            {
                var objetoanonimo = new
                {
                    mensaje = "no se ha podido crear el usuario por x motivo."
                };
                return new JsonResult(objetoanonimo) { StatusCode = 209 }; 
            }
            return new JsonResult(Usuarioresponse) { StatusCode = 201};
        }


        //// POST: api/Usuarios
        //[HttpPost]
        //public async Task<ActionResult<Usuario>> PostUsuario(UsuarioDTO usuarioDTO)
        //{
        //    var usuario = new Usuario
        //    {
        //        Nombre = usuarioDTO.Nombre,
        //        Apellido = usuarioDTO.Apellido,
        //        CorreoElectronico = usuarioDTO.CorreoElectronico,
        //        Contrasena = usuarioDTO.Contrasena,
        //        FotoPerfil = usuarioDTO.FotoPerfil,
        //        Domicilio = usuarioDTO.Domicilio,
        //        FechaNacimiento = usuarioDTO.FechaNacimiento,
        //        EstadoUsuarioId = usuarioDTO.EstadoUsuarioId,
        //        Token = Token.GenerarToken()
        //    };


        //    _context.Usuarios.Add(usuario);
        //    await _context.SaveChangesAsync();
        //    await VerificacionRegistro.EnviarToken(usuario);
        //    return CreatedAtAction("GetUsuario", new { id = usuario.UsuarioId }, usuario);
        //}


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
                    return new JsonResult(objetoanonimo) { StatusCode = 209 };
                }
            }

            UsuarioResponse = await UsuarioService.PutUsuario(Id, UsuarioDTO);
            
            return new JsonResult(UsuarioResponse) { StatusCode = 201 };
        }


        //// PUT: api/Usuarios/5
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutUsuario(int id, UsuarioDTO usuarioDTO)
        //{
        //    if (id != usuarioDTO.UsuarioId)
        //    {
        //        return BadRequest();
        //    }

        //    var usuario = new Usuario
        //    {
        //        UsuarioId = usuarioDTO.UsuarioId,
        //        Nombre = usuarioDTO.Nombre,
        //        Apellido = usuarioDTO.Apellido,
        //        CorreoElectronico = usuarioDTO.CorreoElectronico,
        //        Contrasena = usuarioDTO.Contrasena,
        //        FotoPerfil = usuarioDTO.FotoPerfil,
        //        Domicilio = usuarioDTO.Domicilio,
        //        FechaNacimiento = usuarioDTO.FechaNacimiento,
        //        EstadoUsuarioId = usuarioDTO.EstadoUsuarioId,
        //        Token = usuarioDTO.Token
        //    };

        //    _context.Entry(usuario).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!UsuarioExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        //private bool UsuarioExists(int id)
        //{
        //    return _context.Usuarios.Any(e => e.UsuarioId == id);
        //}
    }
}
