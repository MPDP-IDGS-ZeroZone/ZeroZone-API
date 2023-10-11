using Microsoft.AspNetCore.Mvc;
using ApiTienda.Data;
using ApiTienda.Data.Models;

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
        public IEnumerable<UsuariosSocio> Get()
        {
            return _context.UsuariosSocios.ToList();
        }
    }
}