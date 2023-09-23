
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using TEAyudo_Usuarios.Application.DTO;
using TEAyudo_Usuarios;
using Tools;

namespace TEAyudo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly TEAyudoContext _context;

        public UsuariosController(TEAyudoContext context)
        {
            _context = context;
        }

        // GET: api/Usuarios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetUsuarios()
        {
            return await _context.Usuarios.ToListAsync();
        }

        // GET: api/Usuarios/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> GetUsuario(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);

            if (usuario == null)
            {
                return NotFound();
            }

            return usuario;
        }

        // POST: api/Usuarios
        [HttpPost]
        public async Task<ActionResult<Usuario>> PostUsuario(UsuarioDTO usuarioDTO)
        {
            var usuario = new Usuario
            {
                Nombre = usuarioDTO.Nombre,
                Apellido = usuarioDTO.Apellido,
                CorreoElectronico = usuarioDTO.CorreoElectronico,
                Contrasena = usuarioDTO.Contrasena,
                FotoPerfil = usuarioDTO.FotoPerfil,
                Domicilio = usuarioDTO.Domicilio,
                FechaNacimiento = usuarioDTO.FechaNacimiento,
                EstadoUsuarioId = usuarioDTO.EstadoUsuarioId,
                Token = Token.GenerarToken()
            };


            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();
            await VerificacionRegistro.EnviarToken(usuario);

            return CreatedAtAction("GetUsuario", new { id = usuario.UsuarioId }, usuario);
        }

        // PUT: api/Usuarios/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsuario(int id, UsuarioDTO usuarioDTO)
        {
            if (id != usuarioDTO.UsuarioId)
            {
                return BadRequest();
            }

            var usuario = new Usuario
            {
                UsuarioId = usuarioDTO.UsuarioId,
                Nombre = usuarioDTO.Nombre,
                Apellido = usuarioDTO.Apellido,
                CorreoElectronico = usuarioDTO.CorreoElectronico,
                Contrasena = usuarioDTO.Contrasena,
                FotoPerfil = usuarioDTO.FotoPerfil,
                Domicilio = usuarioDTO.Domicilio,
                FechaNacimiento = usuarioDTO.FechaNacimiento,
                EstadoUsuarioId = usuarioDTO.EstadoUsuarioId,
                Token = usuarioDTO.Token
            };

            _context.Entry(usuario).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsuarioExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        private bool UsuarioExists(int id)
        {
            return _context.Usuarios.Any(e => e.UsuarioId == id);
        }
    }
}
