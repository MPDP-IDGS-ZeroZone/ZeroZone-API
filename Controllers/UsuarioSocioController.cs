using Microsoft.AspNetCore.Mvc;
using ApiTienda.Data;
using ApiTienda.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace ApiTienda.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuarioSocioController : Controller
    {
        private readonly TiendaBdContext _context;
        public UsuarioSocioController(TiendaBdContext context)
        {
            _context = context;
        }
        
        [HttpGet]
        [Authorize]
        public IEnumerable<UsuariosSocio> Get()
        {
            return _context.UsuariosSocios.ToList();
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<UsuariosSocio>> GetUsuarioSocio(int id)
        {
            var usuarioSocio = await _context.UsuariosSocios.FindAsync(id);

            if (usuarioSocio == null)
            {
                return NotFound();
            }

            return usuarioSocio;
        }

        [HttpPost]
        public async Task<ActionResult<UsuariosSocio>> PostUsuarioSocio(UsuariosSocio usuarioSocio)
        {
            _context.UsuariosSocios.Add(usuarioSocio);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUsuarioSocio), new { id = usuarioSocio.Idusuariosocio }, usuarioSocio);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> PutUsuarioSocio(int id, UsuariosSocio usuarioSocio)
        {
            if (id != usuarioSocio.Idusuariosocio)
            {
                return BadRequest();
            }

            _context.Entry(usuarioSocio).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsuarioSocioExists(id))
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

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteUsuarioSocio(int id)
        {
            var usuarioSocio = await _context.UsuariosSocios.FindAsync(id);

            if (usuarioSocio == null)
            {
                return NotFound();
            }

            _context.UsuariosSocios.Remove(usuarioSocio);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UsuarioSocioExists(int id)
        {
            return _context.UsuariosSocios.Any(e => e.Idusuariosocio == id);
        }
    }
}