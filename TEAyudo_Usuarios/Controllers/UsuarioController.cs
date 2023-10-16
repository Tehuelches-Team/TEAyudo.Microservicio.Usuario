
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using TEAyudo_Usuarios;
using Tools;
using Application.Interface;
using Application.Model.Response;
using Application.Model.DTO;

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
                return Ok(ObjetoAnonimo);
            }
            return Ok(ListaResponse);
        }
        //// GET: api/Usuarios
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Usuario>>> GetUsuarios()
        //{
        //    return await _context.Usuarios.ToListAsync();
        //}




        ////[HttpGet("{Id}")]
        ////public async Task<ActionResult<Usuario>> GetUsuarioById(int Id) 
        ////{
        ////    UsuarioResponse? UsuarioResponse = await UsuarioService.GetUsuarioById(Id);
        ////    if (UsuarioResponse == null)
        ////    {
        ////        var ObjetoAnonimo = new
        ////        {
        ////            Mensaje = "No hay usuarios registrados actualmente."
        ////        };
        ////        return new JsonResult(ObjetoAnonimo) { StatusCode = 204}; //no se si el 204 hace que vuelva vacio y no muestre el objeto
        ////    }
        ////    return Ok(UsuarioResponse);
        ////}


        //// GET: api/Usuarios/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<Usuario>> GetUsuario(int id)
        //{
        //    var usuario = await _context.Usuarios.FindAsync(id);

        //    if (usuario == null)
        //    {
        //        return NotFound();
        //    }

        //    return usuario;
        //}




        ////[HttpPost]
        ////public async Task<IActionResult> PostUsuario(UsuarioDTO UsuarioDTO) 
        ////{
        ////    UsuarioResponse? UsuarioResponse = await UsuarioService.PostUsuario(UsuarioDTO);
        ////    if (UsuarioResponse == null)
        ////    {
        ////        var ObjetoAnonimo = new
        ////        {
        ////            Mensaje = "No se ha podido crear el usuario por x motivo."
        ////        };
        ////        return new JsonResult(ObjetoAnonimo) { StatusCode = 209 }; 
        ////    }
        ////    return new JsonResult(UsuarioResponse) { StatusCode = 201};
        ////}



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


        ////[HttpPut("{Id}")]
        ////public async Task<IActionResult> PutUsuario(int Id, UsuarioDTO UsuarioDTO) 
        ////{
        ////    UsuarioResponse? UsuarioResponse = await UsuarioService.PutUsuario(Id, UsuarioDTO);
        ////    if (UsuarioResponse == null)
        ////    {
        ////        var ObjetoAnonimo = new
        ////        {
        ////            Mensaje = "No se pudo encontrar el usuario Id = " + Id
        ////        };
        ////        return new JsonResult(ObjetoAnonimo) { StatusCode = 404};
        ////    }
        ////    return new JsonResult(UsuarioResponse) { StatusCode = 201 };
        ////}

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
