using AlmarchivosBackend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AlmarchivosBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class loginController : ControllerBase

    {
        private readonly AlmarchivosContext _context;

        public loginController(AlmarchivosContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Login(Login model)
        {
            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Usuario1 == model.Usuario);

            if (usuario == null || usuario.Contraseña != EncriptadorMD5.EncriptarMD5(model.Contraseña))
            {
                return Unauthorized(new { mensaje = "Usuario o contraseña incorrectos." });
            }

            return Ok(new { mensaje = "Autenticación exitosa.",
                idPersona = usuario.IdPersona
            }); 
        }

    }
}
