using AlmarchivosBackend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AlmarchivosBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class personasController : ControllerBase
    {
        private readonly AlmarchivosContext _context;

        public personasController(AlmarchivosContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Persona>>> getPersona()
        {
            return await _context.Personas.ToListAsync();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Persona>> GetPersona(int id)
        {
            var persona = await _context.Personas.FindAsync(id);

            if (persona == null)
            {
                return NotFound("Estudiante no encontrado.");
            }

            return persona;
        }

        [HttpPost]
        public async Task<ActionResult<Persona>> CreatePersona(Persona persona)
        {
            _context.Personas.Add(persona);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(getPersona), new { id = persona.IdPersona }, persona);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUsuario(int id, Persona usuario)
        {
            // Verifica si el ID del usuario coincide con el ID proporcionado
            if (id != usuario.IdPersona)
            {
                return BadRequest("El ID del usuario no coincide.");
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
                if (!_context.Personas.Any(e => e.IdPersona == id))
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

    }
}
