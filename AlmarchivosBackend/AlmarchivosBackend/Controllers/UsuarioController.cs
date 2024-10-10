using AlmarchivosBackend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace AlmarchivosBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuarioController : ControllerBase

        {

        private readonly AlmarchivosContext _context;

        public UsuarioController (AlmarchivosContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetUsuario()
        {
            return await _context.Usuarios.ToListAsync();
        }

        // GET: /Usuario/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> GetUsuario(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);

            if (usuario == null)
            {
                return NotFound("Estudiante no encontrado.");
            }

            return usuario;
        }

        // POST: /Usuario
        [HttpPost]
        public async Task<IActionResult> CreateUsuario(Usuario usuario)
        {
            // Encriptar la contraseña usando MD5
            usuario.Contraseña = EncriptadorMD5.EncriptarMD5(usuario.Contraseña);

            // Guardar el usuario en la base de datos
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUsuario), new { id = usuario.IdUsuario }, usuario);
        }

        // PUT: /Estudiante/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUsuario(int id, Usuario usuario)
        {
            // Verifica si el ID del usuario coincide con el ID proporcionado
            if (id != usuario.IdUsuario)
            {
                return BadRequest("El ID del usuario no coincide.");
            }

            // Verifica si la contraseña ha sido actualizada
            if (!string.IsNullOrEmpty(usuario.Contraseña))
            {
                // Encripta la nueva contraseña
                usuario.Contraseña = EncriptadorMD5.EncriptarMD5(usuario.Contraseña);
            }

            // Marca la entidad como modificada
            _context.Entry(usuario).State = EntityState.Modified;

            try
            {
                // Guarda los cambios en la base de datos
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                // Maneja excepciones de concurrencia
                if (!_context.Usuarios.Any(e => e.IdUsuario == id))
                {
                    return NotFound("Usuario no encontrado.");
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }


        // DELETE: /Estudiante/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuario(int id)
        {
            var estudiante = await _context.Usuarios.FindAsync(id);

            if (estudiante == null)
            {
                return NotFound("Estudiante no encontrado.");
            }

            _context.Usuarios.Remove(estudiante);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("usuarioper/{id}")]
        public async Task<ActionResult<IEnumerable<Usuario>>> materiasprofesor(int id)
        {
            var query = @"
       select * from Usuario where id_Persona  = @id;
    ";

            var result = await _context.Set<Usuario>()
                .FromSqlRaw(query, new SqlParameter("@id", id))
                .ToListAsync();

            return Ok(result);
        }
    }
}
